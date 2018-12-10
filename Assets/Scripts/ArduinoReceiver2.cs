
/* not using this is dont think

using UnityEngine;
using System;
using System.Collections;
using System.IO.Ports;
using UnityEngine.SceneManagement;

//when tidying code https://dailydotnettips.com/back-to-basic-difference-between-int-parse-and-int-tryparse/


//would be better threaded

//https://www.alanzucconi.com/2016/12/01/asynchronous-serial-communication/


//can this class be static it is only doing one thing but may need instance so can move data around
//then changing scene wouldnt matter
// it will probably need to be singleton otherwise

//if arduino plugged in and i run several tests will it still be open could this be an issue

//this class means i now have double copy of some statics

public class arduinoReceiver2 : MonoBehaviour { // can i make it a static class


	private static string port = "COM4" ;
	private static int baudrate =  115200; 
	public static int[,] conversionArray = new int[8,2]; //  can ask to receive nothing
	private static int[] rawPinOutPut = new int[8];
    private static SerialPort stream;


    private int scenePersisting; 


     void Awake(){//singleton

		int numSPWithThisSceneBuildNum = 0;
		scenePersisting = SceneManager.GetActiveScene().buildIndex;
		arduinoReceiver2[] aR = FindObjectsOfType<arduinoReceiver2>();   
			foreach(arduinoReceiver2 arduino in aR){
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

	//this code only works once i dont know why but once stream is opened even
	//though i redefine stream and close it fails on opening?! 
	//doesnt need to be run twice so just turning buttons off once worked
	//but feel this is the clue for other errors when i need to restart the stream

	bool BaudCommWorked = true;
	           
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


	
 
    
    //i can call back and use the callback to fill the shipArray but can i remove the call back and replace callback(data string) with 
    //the code to apply it to the ships within the asychronouseread method

    private IEnumerator AsynchronousReadFromArduino()
    {
         

      
            try
            {
				 // i added
				WriteToArduino();
                string dataString = stream.ReadLine();
			    rawPinOutPut = Array.ConvertAll(dataString.Split(' '), int.Parse);
				
							
for(int i=0; i<8; i++){
	for(int j=0; j<8; j++){
		if(conversionArray[j,0] == i){
							if(rawPinOutPut.Length<8){ Debug.Log("chopped array but not long enoug"); }
							 //not a good solve also didnt work when below where rawPinOutput set! is two coroutines running and its being altered during the for loop
			conversionArray[j,1] = rawPinOutPut[i]; //starteed getting out of range errorsS TODO
									
									break; //test if break does anything if not need to export to a method and use return but not sure how that effect coroutine
									}}}

for(int i=0; i<8; i =i+2 ){
					if(conversionArray[i,0] ==8){GameManager.shipPlayerSettingsAr[i/2].SetmyLeftVolt(0);}else  //TODO not sure this works as didnt seem to reset
					{GameManager.shipPlayerSettingsAr[i/2].SetmyLeftVolt(conversionArray[i,1]);}} //apply the lefts blanks other wise


				for(int i=1; i<8; i =i+2){
					if(conversionArray[i,0]==8){GameManager.shipPlayerSettingsAr[(i-1)/2].SetmyRightVolt(0);}else
					{GameManager.shipPlayerSettingsAr[(i-1)/2].SetmyRightVolt(conversionArray[i,1]);}} //applytherights

		 			
            }

			catch(System.IO.IOException ){ //ive added this if it slows it try this UnauthorizedAccessException
            Debug.Log("just handled an io exception");


            Close();
            OpenArduinoStream(); //this causes error in own right
			
            }

			catch (TimeoutException)
            {          
            }



		yield return null; 

 				
    }



public void startUpdatingInputs(){

GameManager.useArduinoMethod();

Debug.Log("there should be only one coroutine and thats me");	
		
StartCoroutine
(
			
    AsynchronousReadFromArduino
    (   
				
));

}


	


	






}
*/