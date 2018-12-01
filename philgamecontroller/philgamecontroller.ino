#include <SerialCommands.h>


SerialCommand sCmd;

//ideal code would just send i think rather than ask then send as always asking for the same thing
//ideal code would send a int array


#define arduinoLED  LED_BUILTIN  // Arduino LED on board

void setup() {
    pinMode(arduinoLED,OUTPUT);      // Configure the onboard LED for output
  digitalWrite(arduinoLED,LOW);    // default to LED off
  Serial.begin(115200); //changed from 9600 pt
  while (!Serial);

  sCmd.addCommand("DATA0", data0Handler);
  sCmd.addCommand("DATA1", data1Handler);
  sCmd.addCommand("DATA2", data2Handler);
  sCmd.addCommand("DATA3", data3Handler);
  sCmd.addCommand("DATA4", data4Handler);
  sCmd.addCommand("DATA5", data5Handler);
  sCmd.addCommand("DATA6", data6Handler);
  sCmd.addCommand("DATA7", data7Handler);
  sCmd.addCommand("ECHO", echoHandler);
  sCmd.setDefaultHandler(errorHandler);
  sCmd.addCommand("ON",LED_on);       // Turns LED on
  sCmd.addCommand("OFF",LED_off);        // Turns LED off
  sCmd.addCommand("ALLDATA", allDataHandler); 
}

void loop() {
  
  // Your operations here

  if (Serial.available() > 0){
    //Serial.print(Serial.available());
    sCmd.readSerial();
    }
  
  delay(50);  //pt is the changeable
}

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
void allDataHandler()
{
  Serial.println( 

    analogRead(A0)
    +" " +
    analogRead(A1)
    +" " +
    analogRead(A2)
    +" " +
    analogRead(A3)
    +" " +
    analogRead(A4)
    +" " +
    analogRead(A5)
    +" " +    
    analogRead(A6)
    +" " +
    
    analogRead(A7));
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
