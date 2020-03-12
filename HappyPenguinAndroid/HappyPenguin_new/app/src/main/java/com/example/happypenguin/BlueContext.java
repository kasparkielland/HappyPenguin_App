package com.example.happypenguin;

import android.bluetooth.BluetoothAdapter;
import android.bluetooth.BluetoothDevice;
import android.bluetooth.BluetoothSocket;
import android.os.Handler;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.util.ArrayList;
import java.util.Set;
import java.util.UUID;

public class BlueContext
{
    private static BluetoothAdapter mBluetoothAdapter;
    private static BluetoothSocket mmSocket;
    private static BluetoothDevice mmDevice;
    public static OutputStream mmOutputStream;
    private static InputStream mmInputStream;
    private static Thread workerThread;
    private static byte[] readBuffer;
    private static int readBufferPosition;
    //private static int counter;
    private static volatile boolean stopWorker;

    public static String findBT()
    {
        String statusMessage = "";
        try
        {
            mBluetoothAdapter = BluetoothAdapter.getDefaultAdapter();
            if (mBluetoothAdapter == null)
            {
                statusMessage = "No bluetooth adapter available";

                return statusMessage;
            }

            Set<BluetoothDevice> pairedDevices = mBluetoothAdapter.getBondedDevices();
            if (pairedDevices.size() > 0)
            {
                for (BluetoothDevice device : pairedDevices)
                {
                    if (device.getName().equals("MLT-BT05"))
                    {
                        mmDevice = device;
                        break;
                    }
                }
            }
            statusMessage  ="Bluetooth Device Found";
        }
        catch(Exception ex)
        {
            statusMessage = ex.toString();
        }

        return statusMessage;
    }

    public static void openBT() throws IOException
    {
        UUID uuid = UUID.fromString("00001101-0000-1000-8000-00805F9B34FB"); //Standard SerialPortService ID

        mmSocket = mmDevice.createRfcommSocketToServiceRecord(uuid);

        mmSocket.connect();


        mmOutputStream = mmSocket.getOutputStream();
        mmInputStream = mmSocket.getInputStream();
        beginListenForData();
    }

    public static void beginListenForData()
    {
        final Handler handler = new Handler();
        final byte delimiter = 10; //This is the ASCII code for a newline character

        stopWorker = false;
        readBufferPosition = 0;
        readBuffer = new byte[1024];

        workerThread = new Thread(new Runnable()
        {
            public void run() {

                String[] values = new String[10];
                int stringCounter = 0;

                while (!Thread.currentThread().isInterrupted() && !stopWorker) {
                    try {
                        int bytesAvailable = mmInputStream.available();

                        if (bytesAvailable > 0) {

                            byte[] packetBytes = new byte[bytesAvailable];
                            mmInputStream.read(packetBytes);

                            for (int i = 0; i < bytesAvailable; i++) {
                                byte b = packetBytes[i];

                                if ((int) b != 13 && (int) b != 10) {
                                    values[stringCounter] = Character.toString((char) (int) b);
                                }

                                if ((int) b == delimiter) {
                                    byte[] encodedBytes = new byte[readBufferPosition];
                                    System.arraycopy(readBuffer, 0, encodedBytes, 0, encodedBytes.length);
                                    StringBuilder tempBuilder = new StringBuilder();

                                    String[] withoutNull = removeNull(values);

                                    for (int x = 0; x < withoutNull.length; x++) {
                                        tempBuilder.append(withoutNull[x]);
                                    }


                                    final String data = tempBuilder.toString();//new String(encodedBytes, "ISO-8859-1");
                                    readBufferPosition = 0;

                                    values = new String[10];
                                    stringCounter = 0;

                                    handler.post(new Runnable() {
                                        public void run() {

                                            DataContext.sendDataToMqtt(data.getBytes());

                                            try {
                                                float value = Float.parseFloat(data);
                                                sendToGraph(value);
                                                DataContext.getDataValue(value, false);
                                            } catch (NumberFormatException fx) {
                                            }
                                        }
                                    });
                                } else {
                                    readBuffer[readBufferPosition++] = b;
                                    stringCounter++;
                                }
                            }

                        }
                    }
                    catch (IOException ex)
                    {
                        stopWorker = true;
                    }
                }

            }

        });

        workerThread.start();
    }

    private static void sendToGraph(float data)
    {
        DataContext.getDataValue(data, false);
    }

    private static String[] removeNull(String[] values)
    {
        ArrayList<String> removed = new ArrayList<String>();

        for(String s : values)
        {
            if(s != null)
            {
                removed.add(s);
            }
        }

        return removed.toArray(new String[0]);
    }

    public static void closeBT() throws IOException
    {
        stopWorker = true;
        mmOutputStream.close();
        mmInputStream.close();
        mmSocket.close();
    }
}
