package com.example.happypenguin;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.util.Log;
import android.view.View;

import com.jjoe64.graphview.GraphView;
import com.jjoe64.graphview.series.DataPoint;
import com.jjoe64.graphview.series.LineGraphSeries;

import java.util.Timer;
import java.util.TimerTask;

public class GraphActivity extends AppCompatActivity
{
    int x = 0;
    int xMax = 75;
    DataPoint[] dataValues =  new DataPoint[xMax+1];
    GraphView graph;
    LineGraphSeries<DataPoint> series = new LineGraphSeries<>();
    Timer executor = new Timer();

    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_graph);
        graph = findViewById(R.id.weightGraph);
        graph.setVisibility(View.VISIBLE);
        graph.getViewport().setXAxisBoundsManual(true);
        graph.getViewport().setMaxX(xMax+1);
        graph.addSeries(series);


        executor.scheduleAtFixedRate(new TimerTask()
        {
            public void run() {
                drawGraph();
            }
        }, 0, 1000);
    }

    @Override
    protected void onStop()
    {
        super.onStop();
        executor.cancel();
    }

    private void drawGraph()
    {

        try
        {
            float value = DataContext.getDataValue(0, true);
            series.appendData(new DataPoint(x,value),false, xMax + 1);
            //graph.addSeries(series);

            x++;

            if(x == xMax)
            {
                series.resetData(new DataPoint[]{new DataPoint(0,0)});
                x = 0;
                graph.getViewport().setMaxX(xMax+1);
            }
        }
        catch (NumberFormatException fx)
        {
        }
        catch(IllegalArgumentException ex)
        {
        }
    }
}
