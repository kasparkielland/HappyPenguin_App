package com.example.happypenguin;

import android.Manifest;
import android.content.Context;
import android.os.Environment;
import android.util.Log;

import org.eclipse.paho.client.mqttv3.MqttClient;
import org.eclipse.paho.client.mqttv3.MqttConnectOptions;
import org.eclipse.paho.client.mqttv3.MqttException;
import org.eclipse.paho.client.mqttv3.MqttMessage;
import org.eclipse.paho.client.mqttv3.persist.MemoryPersistence;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.util.concurrent.locks.ReentrantLock;

import java.math.*;

public class DataContext
{
    private static String _broker = "tcp://m23.cloudmqtt.com";
    private static String _port = "10843";
    private static String _user = "srsrlhei";
    private static String _password = "-C029zRiAP6h";
    private static String _topic = "sensor/message";
    private static String _clientId = "VektData";
    private static int _qos = 0;
    private static float dataValue;
    private final static String fileName = "mqttDetails.txt";
    static ReentrantLock lock = new ReentrantLock();

    private static MemoryPersistence _persistence = new MemoryPersistence();
    private static MqttClient _client;

    public static String getBroker()
    {
        return _broker;
    }

    public static void setBroker(String broker)
    {
        _broker = broker;
    }

    public static String getPort()
    {
        return _port;
    }

    public static void setPort(String port)
    {
        _port = port;
    }

    public static String getUser()
    {
        return _user;
    }

    public static void setUser(String user)
    {
        _user = user;
    }

    public static String getPassword()
    {
        return _password;
    }

    public static void setPassword(String password)
    {
        _password = password;
    }

    public static float getDataValue(float data, boolean get)
    {

        lock.lock();

        try
        {

            if(get)
            {
                return dataValue;
            }
            else
            {
                data = decrypt(data);
                dataValue = data;
                return dataValue;
            }
        }
        finally
        {
            lock.unlock();
        }
    }

    public static void setDataValue(float data)
    {
        dataValue = data;
    }


    public static boolean getData()
    {
        try
        {
            File storage = Environment.getExternalStorageDirectory();
            File file = new File(storage.getAbsolutePath(), fileName);

            if(!file.exists())
            {
                return false;
            }

            BufferedReader bReader = new BufferedReader(new FileReader(file));

            String receivedString = "";
            receivedString = bReader.readLine();

            bReader.close();

            String[] data = receivedString.split(";");
            setData(data[0], data[1], data[2], data[3]);
        }
        catch(IOException ex) {
            return false;
        }

        return true;
    }

    public static void setData(String broker, String port, String user, String password)
    {
        setBroker(broker);
        setPort(port);
        setUser(user);
        setPassword(password);
    }

    public static void StoreData()
    {
        try
        {
            File storage = Environment.getExternalStorageDirectory();
            File file = new File(storage.getAbsolutePath(), fileName);

            FileWriter writer = new FileWriter(file);
            writer.write(getBroker() + ";"+ getPort()+";"+getUser()+";"+getPassword());
            writer.close();
        }
        catch(IOException ex)
        {
        }
    }

    public static void sendDataToMqtt(byte[] data)
    {
        MqttMessage message = new MqttMessage(data);
        message.setQos(_qos);
        try {
            if (_client != null && _client.isConnected()) {
                _client.publish(_topic, message);
            }
        } catch (MqttException ex) {
        }
    }

    public static String connectToMqtt()
    {
        try
        {
            MqttConnectOptions connOps = new MqttConnectOptions();
            connOps.setCleanSession(true);

            if(DataContext.getData())
            {
                _client = new MqttClient("tcp://" + getBroker() + ":" + getPort(), _clientId, _persistence);
                connOps.setUserName(getUser());
                connOps.setPassword(getPassword().toCharArray());
            }
            else
            {
                //default settings
                _client = new MqttClient(_broker + ":" + _port, _clientId, _persistence);
                connOps.setUserName(_user);
                connOps.setPassword(_password.toCharArray());
                Log.i("GotDataFrom", "default");
            }

            _client.connect(connOps);
            return "MQTT connected";
        }
        catch(MqttException ex)
        {
            return "Could not connect MQTT";
        }
    }

    public static String disconnectMqtt()
    {
        try {
            _client.disconnect();
            return "MQTT closed";
        }
        catch(MqttException ex)
        {
            return "Could not close MQTT connection";
        }
    }

    //Primtallene
    static float p = 69697;
    static float q = 420691;

    //Kalkulerer n
    static float n=p*q;

    //Kalkulerer phi
    static float phi= (p-1)*(q-1);

    //Danner dekrypteringsnøkkelen d ved hjelp av forhåndsbestemt e = 107
    static float e = 7;
    static float d1 = 1 / e;
    static float d = d1 % phi;

    private static Float decrypt(Float data){


        // Dekrypterer meldingen m
        data = (float) Math.pow(data, d);
        data = data % n;

        return data;

    }
}
