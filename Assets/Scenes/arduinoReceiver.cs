using UnityEngine;
using System;
using System.Collections;
using System.IO.Ports;

//currently only working on 9600 and get an io exeption acess denied on trying to open yet still works

//https://www.alanzucconi.com/2016/12/01/asynchronous-serial-communication/
//still getting a 60% drop in fps
//if arduino instead of having which pin being requested simply answered with all of them in one string or a string array
//i could just pick what i need this end instead of asyschronously asking it lots of things where i may get different answer if answers in different order to questions
//can this class be static it is only doing one thing but may need instance so can move data around
//then changing scene wouldnt matter
// it will probably need to be singleton otherwise

public class arduinoReceiver : MonoBehaviour {

	[Tooltip("The serial port where the Arduino is connected")]
    public string port = "COM4";
    /* The baudrate of the serial port. */
    [Tooltip("The baudrate of the serial port")]
	public int baudrate =  9600;  //  115200;;;//ive changed it at the computer side
	private string[] pins = {"DATA0", "DATA1", "DATA2", "DATA3", "DATA4", "DATA5", "DATA6", "DATA7" };

    private SerialPort stream;

    private void OpenArduinoStream () {
        // Opens the serial port
        stream = new SerialPort(port, baudrate);
        stream.ReadTimeout = 1;
        stream.Open();
        //this.stream.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
    }

    private void WriteToArduino(string data)
    {
        // Send the request
        stream.WriteLine(data);
        stream.BaseStream.Flush();
    }

	private void WriteToArduinoTest(string data)
    {
        // Send the request
        stream.WriteLine("DATA0");

        stream.BaseStream.Flush();
    }

   private string ReadFromArduino(int timeout = 100)
    {
        stream.ReadTimeout = timeout;
        try
        {
            return stream.ReadLine();
        }
        catch (TimeoutException)
        {
        Debug.Log("timeout in readfrom arduino ");
             return null;
        }
    }
    

    private IEnumerator AsynchronousReadFromArduino(Action<string> callback, Action fail = null, float timeout = float.PositiveInfinity)
    {
        DateTime initialTime = DateTime.Now;
        DateTime nowTime;
        TimeSpan diff = default(TimeSpan);
		
        string dataString = null;

        do
        {
			WriteToArduino("DATA0"); // i added this so here i just need to have all my coroutines writing and reading but dont know if will get mixed up

            // A single read attempt
            try
            {
				 // i added
                dataString = stream.ReadLine();
                //im not specifying what i want to read because im not writing would it be better to code arduino to provide all results one string
            }
            catch (TimeoutException)
            {
                dataString = null;
            }

            if (dataString != null)
            {
                callback(dataString);
                yield return null;
            } else
                yield return new WaitForSeconds(0.05f); //is frame rate dropping 60% because currently not finding anything
                Debug.Log("waited");
            nowTime = DateTime.Now;
            diff = nowTime - initialTime;

        } while (diff.Milliseconds < timeout);

        if (fail != null)
            fail();
        yield return null;
    }

    private void Close()
    {
        stream.Close();
    }


public void startUpdatingInputs(){

//put this in a foreach loop
	

StartCoroutine
(
			
    AsynchronousReadFromArduino
    (   
				
			//(string data)=>	WriteToArduinoTest(data),
    (string s) => Debug.Log(s),     // Callback
        () => Debug.LogError("Error!"), // Error callback
        10000f                          // Timeout (milliseconds)
    )
);

}


public void setOrderPinDataRequestedToMatchBikeOrder(string[] newDatasOrder){
}
	void Start(){      OpenArduinoStream(); //needs to be seperate from everything else or will cause an error when they check too soon}
	}
public void writeReadTest(){
  

		WriteToArduino("DATA0");
		string iRead = ReadFromArduino(100);
		Debug.Log(iRead);

}

}
