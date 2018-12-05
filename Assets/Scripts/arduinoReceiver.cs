using UnityEngine;
using System;
using System.Collections;
using System.IO.Ports;
using UnityEngine.SceneManagement;
//when tidying code https://dailydotnettips.com/back-to-basic-difference-between-int-parse-and-int-tryparse/

//change the times to frame time, and see if change it to physics update time if can get it to optimise to minimum reads that gives good responsiveness
//would be better threaded

//https://www.alanzucconi.com/2016/12/01/asynchronous-serial-communication/


//can this class be static it is only doing one thing but may need instance so can move data around
//then changing scene wouldnt matter
// it will probably need to be singleton otherwise

//if arduino plugged in and i run several tests will it still be open could this be an issue



//the old baud code only works once i dont know why but once stream is opened even
	//though i redefine stream and close it fails on opening?! 
	//doesnt need to be run twice so just turning buttons off once worked
	//but feel this is the clue for other errors when i need to restart the stream
	//had several issues with closing reopening the stream believe is a source of my errors
	//also issue writing when already written reading when already read i believe
//could this be what upsets it if change this 	stream = new  SerialPort(port, baudrate);

//should the coroutine return yield the numbers maybe to a method that updates the ships


public class arduinoReceiver : MonoBehaviour { // can i make it a static class

	//i believe changing to statics stopped some errors
	private static string port = "COM4" ;
	private static int baudrate =  115200; 
	public static int[,] conversionArray = new int[8,2]; //  can ask to receive nothing
	private static int[] rawPinOutPut = new int[8];
    private static SerialPort stream;


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


	public bool isBaudCommRight(){ //always catches now



	bool BaudCommWorked = true;

	           /*    //replace with one run through of the coroutine code to check if returns waiting or a number
           try
            {
			
			stream = new  SerialPort(port, baudrate);
			//Close();
			OpenArduinoStream(); //found if this happens withouther things it fails
			WriteToArduino();
			stream.ReadLine();	               
            }
            catch {
			Debug.Log("BaudCommWFailed = "+ BaudCommWorked);
			BaudCommWorked = false;}
	*/ 
		OpenArduinoStream(); //del later
	Debug.Log("BaudCommWorked = "+ BaudCommWorked);
	return BaudCommWorked;
	}


    private void OpenArduinoStream () {//error handler ran open and then said port didnt exist but it still worked
        // Opens the serial port
        stream = new  SerialPort(port, baudrate);
        stream.ReadTimeout = 100; //should it be higher ive changed 1 to 100
        stream.Open();
     }

	private void Close()
    {
        stream.Close();
    }


    private void WriteToArduino()
    {

        stream.WriteLine("ALLDATA");
        stream.BaseStream.Flush(); 
    }


    private IEnumerator AsynchronousReadFromArduino()
    {
            while(true){ 

			DateTime initialTime = DateTime.Now;
        DateTime nowTime;
        TimeSpan diff = default(TimeSpan);
			bool readSuccesfully = false;  

            try
            {
			
				WriteToArduino(); //if the read fails it will write twice before a read will that cause an error? do they need seperate trys
                string dataString = stream.ReadLine();
                Debug.Log(dataString);
			    rawPinOutPut = Array.ConvertAll(dataString.Split(' '), int.Parse);
				
//export to a method							
for(int i=0; i<8; i++){
	for(int j=0; j<8; j++){
		if(conversionArray[j,0] == i){

							if(rawPinOutPut.Length<8){ Debug.Log("chopped array but not long enoug"); }
						//TODO not a good solve also didnt work when below where rawPinOutput set! is two coroutines running and its being altered during the for loop

			conversionArray[j,1] = rawPinOutPut[i];
									
									break; //test if break does anything if not need to export to a method and use return but not sure how that effect coroutine
									}}}

for(int i=0; i<8; i =i+2 ){
					if(conversionArray[i,0] ==8){GameManager.shipPlayerSettingsAr[i/2].SetmyLeftVolt(0);}else  //TODO not sure this works as didnt seem to reset
					{GameManager.shipPlayerSettingsAr[i/2].SetmyLeftVolt(conversionArray[i,1]);}} //apply the lefts blanks other wise


				for(int i=1; i<8; i =i+2){
					if(conversionArray[i,0]==8){GameManager.shipPlayerSettingsAr[(i-1)/2].SetmyRightVolt(0);}else
					{GameManager.shipPlayerSettingsAr[(i-1)/2].SetmyRightVolt(conversionArray[i,1]);}} //applytherights

				//in old code got a wait every read because read and then returned straight away
				//this would mean as soon as it is possibly ready it will read again
				//i dont think i need it to be so fast
				Debug.Log("read");
				//experiment with this
               

				readSuccesfully = true;  
		 			
            }

			catch(System.IO.IOException ){ //ive added this if it slows it try this UnauthorizedAccessException
            Debug.Log("just handled an io exception so just going to ask the coroutine to receive null and check again quick");


         //   Close();
         //   OpenArduinoStream(); //this causes error in own right
				//yield return null; 
				readSuccesfully = false;      
            }

			catch (TimeoutException)
            { 
				Debug.Log("just handled an timeout exception so just going to ask the coroutine to receive null and check again quick");
				//yield return null; 
				readSuccesfully = false;         
            }

            if(readSuccesfully){
			Debug.Log("im at the end of the coroutine so gonna wait ");
				yield return new WaitForSeconds(0.05f);   
				nowTime = DateTime.Now;
            diff = nowTime - initialTime; }
            else{Debug.Log("didnt read succesfully better try again quick");yield return null;} 

 				
    }}



public void startUpdatingInputs(){

GameManager.useArduinoMethod();

Debug.Log("there should be only one coroutine and thats me");	
		
StartCoroutine
(
			
    AsynchronousReadFromArduino
    (   
				
));

}


	

	/*
	using UnityEngine;
using System;
using System.Collections;
using System.IO.Ports;
using UnityEngine.SceneManagement;

//when tidying code https://dailydotnettips.com/back-to-basic-difference-between-int-parse-and-int-tryparse/


//currently only working on 9600 and get an io exeption acess denied on trying to open yet still works

//https://www.alanzucconi.com/2016/12/01/asynchronous-serial-communication/
//still getting a 60% drop in fps
//if arduino instead of having which pin being requested simply answered with all of them in one string or a string array
//i could just pick what i need this end instead of asyschronously asking it lots of things where i may get different answer if answers in different order to questions
//can this class be static it is only doing one thing but may need instance so can move data around
//then changing scene wouldnt matter
// it will probably need to be singleton otherwise

public class arduinoReceiver : MonoBehaviour { // can i make it a static class




/*
	[Tooltip("The serial port where the Arduino is connected")]
	public string port = "COM4" ;
    
    [Tooltip("The baudrate of the serial port")]
	private int baudrate =  115200;  //  115200;;;//ive changed it at the computer side
	public static int[,] conversionArray = new int[8,2]; //  can ask to receive nothing
	private static string[] pins = {"DATA0", "DATA1", "DATA2", "DATA3", "DATA4", "DATA5", "DATA6", "DATA7" };
	private static int[] rawPinOutPut = new int[8];
	private static int[] LRRYGBArrangedPinOut = new int[8];
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
        stream.WriteLine(data); //run during battle caused io error did it call it twice before using it?!

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
			WriteToArduinoTest("ALLDATA"); // works with or without flush i added this so here i just need to have all my coroutines writing and reading but dont know if will get mixed up

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



 				rawPinOutPut = Array.ConvertAll(dataString.Split(' '), int.Parse);
				if(rawPinOutPut.Length<8){ Debug.Log("chopped array but not long enoug"); yield return null;} //not a good solve
 				//conversionArray
 				//LRRYGBArrangedPinOut


///then when i have my array of numbers they will need reordering
//think using an enum some how would make code better to read
//idont want to change order of ship array

//have a middle array so pinArray convertArray shipArray
//the convert array has the desired order e,g [0] = 7 [1]=4 etc
//so foreach element in pin array the index holding that pin index receives the value
//make it 2d as if it replaces its value then if any read happens to =0 it might get found
				
for(int i=0; i<8; i++){
	for(int j=0; j<8; j++){
		if(conversionArray[j,0] == i){
		Debug.Log(" i is " + i + " j is " + j);
		Debug.Log("raw input array is this long " + rawPinOutPut.Length); // is it being updated conversion array while in use? for some reason length 1!

			conversionArray[j,1] = rawPinOutPut[i]; //starteed getting out of range errorsS TODO
									
									break;
									}}}
//if for when i dont want a blank

for(int i=0; i<8; i =i+2 ){
					if(conversionArray[i,0] ==8){GameManager.shipPlayerSettingsAr[i/2].SetmyLeftVolt(0);}else  //TODO not sure this works as didnt seem to reset
					{GameManager.shipPlayerSettingsAr[i/2].SetmyLeftVolt(conversionArray[i,1]);}} //apply the lefts blanks other wise


				for(int i=1; i<8; i =i+2){
					if(conversionArray[i,0]==8){GameManager.shipPlayerSettingsAr[(i-1)/2].SetmyRightVolt(0);}else
					{GameManager.shipPlayerSettingsAr[(i-1)/2].SetmyRightVolt(conversionArray[i,1]);}} //applytherights

                                                        


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

GameManager.useArduinoMethod();

	Debug.Log("there should be only one coroutine and thats me");	
		
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
	


	
public void writeReadTest(){
  

		WriteToArduino("ALLDATA");
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

	

	*/






/*

	//when tidying code https://dailydotnettips.com/back-to-basic-difference-between-int-parse-and-int-tryparse/


//currently only working on 9600 and get an io exeption acess denied on trying to open yet still works

//https://www.alanzucconi.com/2016/12/01/asynchronous-serial-communication/
//still getting a 60% drop in fps
//if arduino instead of having which pin being requested simply answered with all of them in one string or a string array
//i could just pick what i need this end instead of asyschronously asking it lots of things where i may get different answer if answers in different order to questions
//can this class be static it is only doing one thing but may need instance so can move data around
//then changing scene wouldnt matter
// it will probably need to be singleton otherwise
	[Tooltip("The serial port where the Arduino is connected")]
	public string port = "COM4" ;

    [Tooltip("The baudrate of the serial port")]
	private int baudrate =  115200;  //  115200;;;//ive changed it at the computer side
	public static int[,] conversionArray = new int[8,2]; //  can ask to receive nothing
	private static string[] pins = {"DATA0", "DATA1", "DATA2", "DATA3", "DATA4", "DATA5", "DATA6", "DATA7" };
	private static int[] rawPinOutPut = new int[8];
	private static int[] LRRYGBArrangedPinOut = new int[8];
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
        stream.WriteLine(data); //run during battle caused io error did it call it twice before using it?!

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
			WriteToArduinoTest("ALLDATA"); // works with or without flush i added this so here i just need to have all my coroutines writing and reading but dont know if will get mixed up

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



 				rawPinOutPut = Array.ConvertAll(dataString.Split(' '), int.Parse);
				if(rawPinOutPut.Length<8){ Debug.Log("chopped array but not long enoug"); yield return null;} //not a good solve
 				//conversionArray
 				//LRRYGBArrangedPinOut


///then when i have my array of numbers they will need reordering
//think using an enum some how would make code better to read
//idont want to change order of ship array

//have a middle array so pinArray convertArray shipArray
//the convert array has the desired order e,g [0] = 7 [1]=4 etc
//so foreach element in pin array the index holding that pin index receives the value
//make it 2d as if it replaces its value then if any read happens to =0 it might get found
				
for(int i=0; i<8; i++){
	for(int j=0; j<8; j++){
		if(conversionArray[j,0] == i){
		Debug.Log(" i is " + i + " j is " + j);
		Debug.Log("raw input array is this long " + rawPinOutPut.Length); // is it being updated conversion array while in use? for some reason length 1!

			conversionArray[j,1] = rawPinOutPut[i]; //starteed getting out of range errorsS TODO
									
									break;
									}}}
//if for when i dont want a blank

for(int i=0; i<8; i =i+2 ){
					if(conversionArray[i,0] ==8){GameManager.shipPlayerSettingsAr[i/2].SetmyLeftVolt(0);}else  //TODO not sure this works as didnt seem to reset
					{GameManager.shipPlayerSettingsAr[i/2].SetmyLeftVolt(conversionArray[i,1]);}} //apply the lefts blanks other wise


				for(int i=1; i<8; i =i+2){
					if(conversionArray[i,0]==8){GameManager.shipPlayerSettingsAr[(i-1)/2].SetmyRightVolt(0);}else
					{GameManager.shipPlayerSettingsAr[(i-1)/2].SetmyRightVolt(conversionArray[i,1]);}} //applytherights

                                                        


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

GameManager.useArduinoMethod();

	Debug.Log("there should be only one coroutine and thats me");	
		
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
	


	
public void writeReadTest(){
  

		WriteToArduino("ALLDATA");
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




	*/

}
