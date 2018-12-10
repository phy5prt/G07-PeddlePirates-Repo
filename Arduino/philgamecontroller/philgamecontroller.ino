#include <SerialCommand.h>


//io error is happening and causing fail

//ideally would just constantly update my statics without having to have
//the information requested just constantly sending it
//even better as an int array

SerialCommand sCmd;
String space = " ";

#define arduinoLED  LED_BUILTIN  // Arduino LED on board

void setup() {
    pinMode(arduinoLED,OUTPUT);      // Configure the onboard LED for output
  digitalWrite(arduinoLED,LOW);    // default to LED off
  Serial.begin(115200);
  while (!Serial);
 Serial.print("Hi!");
//  sCmd.addCommand("DATA0", data0Handler);
//  sCmd.addCommand("DATA1", data1Handler);
//  sCmd.addCommand("DATA2", data2Handler);
//  sCmd.addCommand("DATA3", data3Handler);
//  sCmd.addCommand("DATA4", data4Handler);
 // sCmd.addCommand("DATA5", data5Handler);
//  sCmd.addCommand("DATA6", data6Handler);
 // sCmd.addCommand("DATA7", data7Handler);
 
  sCmd.addCommand("ALLDATA", allDataHandler);
  
  sCmd.addCommand("ECHO", echoHandler);
 
// sCmd.setDefaultHandler(errorHandler); // error handler is causing an error not sure why and everything is in red
 
  sCmd.addCommand("ON",LED_on);       // Turns LED on
  sCmd.addCommand("OFF",LED_off);        // Turns LED off
    
}

void loop() {
  
  // Your operations here

  if (Serial.available() > 0){
    //Serial.print(Serial.available());
    sCmd.readSerial();
    }
  
  delay(50);  //this changeable?
}


/*
void data0Handler ()
{
  Serial.println(analogRead(A0));
}
void data1Handler ()
{
  Serial.println(analogRead(A1));
}
void data2Handler ()
{
  Serial.println(analogRead(A2));
}
void data3Handler ()
{
  Serial.println(analogRead(A3));
}
void data4Handler ()
{
  Serial.println(analogRead(A4));
}
void data5Handler ()
{
  Serial.println(analogRead(A5));
}
void data6Handler ()
{
  Serial.println(analogRead(A6));
}
void data7Handler ()
{
  Serial.println(analogRead(A7));
}
*/
void allDataHandler ()
{
  Serial.println(
    
    
    analogRead(A0) 
    + space +
    analogRead(A1) 
    + space +
    analogRead(A2)
    + space +
    analogRead(A3)
    + space + 
    analogRead(A4)
    + space +
    analogRead(A5) 
    + space +
    analogRead(A6) 
    + space +
    analogRead(A7)
    
    
    );
}

void LED_on()
{
  Serial.println("LED on"); 
  digitalWrite(arduinoLED,HIGH);  
}

void LED_off()
{
  Serial.println("LED off"); 
  digitalWrite(arduinoLED,LOW);
}


void echoHandler ()
{
  char *arg;
  arg = sCmd.next();
  if (arg != NULL)
    Serial.println(arg);
  else
    Serial.println("nothing to echo");
}

void errorHandler (const char *command)
{
  // Error handling
}
