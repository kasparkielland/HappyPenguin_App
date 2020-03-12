package com.example.happypenguin;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.view.View;
import android.widget.TextView;


public class MqttDetails extends AppCompatActivity {
    TextView broker;// = findViewById(R.id.brokerAddress);
    TextView port;// = findViewById(R.id.brokerPort);
    TextView user;// = findViewById(R.id.userName);
    TextView password;// = findViewById(R.id.password);

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_mqtt_details);

        broker = findViewById(R.id.brokerAddress);
        port = findViewById(R.id.brokerPort);
        user = findViewById(R.id.userName);
        password = findViewById(R.id.password);

        DataContext.getData();

        broker.setText(DataContext.getBroker());
        port.setText(DataContext.getPort());
        user.setText(DataContext.getUser());
        password.setText(DataContext.getPassword());
    }

    public void saveMqttDetails(View view)
    {
        DataContext.setData(broker.getText().toString(), port.getText().toString(), user.getText().toString(), password. getText().toString());

        DataContext.StoreData();
    }


}
