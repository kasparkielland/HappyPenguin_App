package com.example.happypenguin;

import androidx.appcompat.app.AppCompatActivity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.CompoundButton;
import android.widget.TextView;


import java.io.IOException;
import java.util.Timer;
import java.util.TimerTask;

public class MainActivity extends AppCompatActivity {
    TextView myLabel;
    TextView wResult;
    CheckBox sendData;
    Timer executor = new Timer();

    @Override
    public void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        sendData = findViewById(R.id.checkBox);
        Button openButton = findViewById(R.id.open);
        Button closeButton = findViewById(R.id.close);
        myLabel = findViewById(R.id.label);
        wResult = findViewById(R.id.weightResult);

        // check to see if data should be sent to mqtt
        sendData.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener()
        {
            @Override
            public void onCheckedChanged(CompoundButton buttonView, boolean isChecked)
            {
                if(isChecked)
                {
                    connectToMqtt();
                }
                else
                {
                    disconnectMqtt();
                }
            }
        });



        //Open bluetooth connection
        openButton.setOnClickListener(new View.OnClickListener()
        {
            public void onClick(View v)
            {
                try
                {
                    myLabel.setText(BlueContext.findBT());
                    BlueContext.openBT();
                    myLabel.setText("Bluetooth opened");
                }
                catch (IOException ex) {
                    myLabel.setText(ex.toString());
                }
            }
        });


        //Close bluetooth connection
        closeButton.setOnClickListener(new View.OnClickListener()
        {
            public void onClick(View v)
            {
                try
                {
                    BlueContext.closeBT();
                    myLabel.setText("Bluetooth closed");
                }
                catch (IOException ex) {
                    myLabel.setText(ex.toString());
                }
            }
        });

        //read data every 500 ms
        executor.scheduleAtFixedRate(new TimerTask()
        {
            public void run() {
                displayWeight();
            }
        }, 0, 500);
    }


    void connectToMqtt()
    {
        myLabel.setText(DataContext.connectToMqtt());
    }

    void disconnectMqtt()
    {
        myLabel.setText(DataContext.disconnectMqtt());
    }

    void displayWeight()
    {
        wResult.setText(Float.toString(DataContext.getDataValue(0, true)) + " kg");
    }

    public void sendData(View view) throws IOException
    {
        String msg = "a\n";
        BlueContext.mmOutputStream.write(msg.getBytes());
        myLabel.setText("Tare Sent");
    }

    public void setMQTTDetails(View view)
    {
        Intent intent = new Intent(this, MqttDetails.class);
        startActivity(intent);
    }

    public void viewGraph(View view)
    {
        Intent intent = new Intent(this, GraphActivity.class);
        startActivity(intent);
    }
}
