using UnityEngine;
using System;
using System.Collections;
using System.IO.Ports;
using UnityEngine.SceneManagement;

//currently only working on 9600 and get an io exeption acess denied on trying to open yet still works

//https://www.alanzucconi.com/2016/12/01/asynchronous-serial-communication/
//still getting a 60% drop in fps
//if arduino instead of having which pin being requested simply answered with all of them in one string or a string array
//i could just pick what i need this end instead of asyschronously asking it lots of things where i may get different answer if answers in different order to questions
//can this class be static it is only doing one thing but may need instance so can move data around
//then changing scene wouldnt matter
// it will probably need to be singleton otherwise

public class arduinoReceiver : MonoBehaviour { // can i make it a static class

	[Tooltip("The serial port where the Arduino is connected")]
	public string port = "COM4" ;
    /* The baudrate of the serial port. */
    [Tooltip("The baudrate of the serial port")]
	private int baudrate =  9600;  //  115200;;;//ive changed it at the computer side
	int[,] conversionArray = new int[8,2]; //  can ask to receive nothing
	private static string[] pins = {"DATA0", "DATA1", "DATA2", "DATA3", "DATA4", "DATA5", "DATA6", "DATA7" };

    private SerialPort stream;

	private int scenePersisting; 
    void Awake(){//singleton

		int numSPWithThisSceneBuildNum = 0;
		scenePersisting = SceneManager.GetActiveScene().buildIndex;

		arduinoReceiver[] aR = FindObjectsOfType<arduinoReceiver>();   
			foreach(arduinoReceiver arduino in aR){
				if(arduino.scenePersisting ==  SceneManager.GetActiveScene().buildIndex) {numSPWithThisSceneBuildNum++;}else	{Destroy(arduino.gameObject);} }  
		
		if (numSPWithThisSceneBuildNum>1){ Destroy(gameObject);}                              
																																					
		DontDestroyOnLoad(gameObject);		


    }
    public void setBaud(int baud){
    baudrate = baud;
    Debug.Log("baud is " + baudrate);
    }

	public void setCom(string com){
   port = com;
		Debug.Log("com is " + port);
    }


    private void OpenArduinoStream () {
        // Opens the serial port
       stream = new  SerialPort(port, baudrate);
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
        stream.WriteLine(data);

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
    
    //i can call back and use the callback to fill the shipArray but can i remove the call back and replace callback(data string) with 
    //the code to apply it to the ships within the asychronouseread method

    private IEnumerator AsynchronousReadFromArduino(Action<string> callback, Action fail = null, float timeout = float.PositiveInfinity)
    {
        DateTime initialTime = DateTime.Now;
        DateTime nowTime;
        TimeSpan diff = default(TimeSpan);
		
        string dataString = null;

        do
        {
        //this will change to "all"
			WriteToArduinoTest("DATA0"); // works with or without flush i added this so here i just need to have all my coroutines writing and reading but dont know if will get mixed up

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

            //here make the datastring an array
            //and apply to the shipArray
            //for now then later removing debug in calling the coroutine

            /* could try // this would work for flexible lengths for each data
            string values = "1,2,3,4,5,6,7,8,9,10";
string[] tokens = values.Split(',');

int[] convertedItems = Array.ConvertAll<string, int>(tokens, int.Parse);

//same as above
 string s = "1,5,7";
 int[] nums = Array.ConvertAll(s.Split(','), int.Parse);


Or // this method relies on each number being 4 digets e.g. 0000 1023 9990 0012 

int[] numbers=new int[strings.Lenght];


for(int c=0;c<strings.Length;c+4;)

numbers[c/4]=int.TryParse(strings[c]) + int.TryParse(strings[c+1])+ int.TryParse(strings[c+2])+ int.TryParse(strings[c+3]);


///then when i have my array of numbers they will need reordering
//think using an enum some how would make code better to read
//idont want to change order of ship array

have a middle array so pinArray convertArray shipArray
the convert array has the desired order e,g [0] = 7 [1]=4 etc
so foreach element in pin array the index holding that pin index receives the value
//make it 2d as if it replaces its value then if any read happens to =0 it might get found

for(int i=0; i<8; i++){
	for(int j=0; j<8; j++){
		if(conversionArray[j,0] = i){
			coversionArray[j,1] = pinOutPutData[i]; 
									Debug.Log(" i is " +i + " j is " + j + "I'm setting the conversionArray"); 
									break;
									}}}
//if for when i dont want a blank
for(int i=0; i<8; i+2;){
if(ConversionArray[i,1] ==8){shipAr[i/2].left = 0;}else
{shipAr[i/2].left = ConversionArray[i,1];}} //apply the lefts blanks other wise


for(int i=1; i<8; i+2;){
if(ConversionArray[i,1]==8){{shipAr[(i-1)/2].Right =0;}else
{shipAr[(i-1)/2].Right = ConversionArray[i,1];}} //applytherights

                                                        */


                callback(dataString);
                yield return null;
            } else
                yield return new WaitForSeconds(0.05f); //is frame rate dropping 60% because currently not finding anything it was so drop this number when know can
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
    (string s) => Debug.Log(s),     // Callback  - wont need this in future
    //(float[] s)=> for(i=0;i<8;i++){GameManager.ar[i] = s[i]} // or just put it straight in the asynchronous read?
        () => Debug.LogError("Error!"), // Error callback
        10000f                          // Timeout (milliseconds)
    )
);

}

//isnt relevant now
//make static once not using it with buttons for testing preset with an array
//remove the preset array order whren done testing
	public void setOrderPinDataRequestedToMatchBikeOrder(int[] newConOrder){// run as they assign the colours

//pins = newDatasOrder;
//put global

		//just modeling the int the method will take




for(int i =0; i<8;i++){
			conversionArray[i,0] = newConOrder[i]; //this will put the 1st column as the new index order and the second column empty as will hold the values

}
}

	
public void writeReadTest(){
  

		WriteToArduino("DATA0");
		string iRead = ReadFromArduino(100);
		Debug.Log(iRead);

}

	public bool isBaudCommRight(){ //this code only works once i dont know why but once stream is opened even
	//though i redefine stream and close it fails on opening?! doesnt need to be run twice so just turning buttons off once worked

	bool BaudCommWorked = true;
	//test
		
           
            try
            {
				 // i added
			stream = new  SerialPort(port, baudrate);
				Close();
				OpenArduinoStream(); //found if this happens withouther things it fails
				               
            }
            catch {BaudCommWorked = false;}
	

		Debug.Log("BaudCommWorked = "+ BaudCommWorked);
	return BaudCommWorked;


	}

}
