#include <SoftwareSerial.h>
#include<Wire.h>
SoftwareSerial mySerial(10, 11); 
String isim = "Arduino UNO";int sifre = 1234;String uart = "9600,0,0";
#define echoPin 3
#define trigPin 2
float duration, distance;
const int MPU=0x68;  // I2C address of the MPU-6050
int16_t AcX,AcY,AcZ,Tmp,GyX,GyY,GyZ;
 
// value returned is in interval [-32768, 32767] so for normalize multiply GyX and others for gyro_normalizer_factor
// float gyro_normalizer_factor = 1.0f / 32768.0f;
 
void setup(){
   Serial.begin(9600); 
  Serial.println("HC-05 Modul Ayarlaniyor…"); 
  Serial.println("Lutfen 5 sn icinde HC-05 modulun uzerindeki butona basili tutarak baglanti yapiniz."); 
  mySerial.begin(9600); delay(5000); 
  mySerial.print("AT+NAME=");
  mySerial.println(isim); 
  Serial.print("Isim ayarlandi: "); 
  Serial.println(isim); 
  delay(1000); 
  mySerial.print("AT+PSWD="); 
  mySerial.println(sifre); 
  Serial.print("Sifre ayarlandi: "); 
  Serial.println(sifre); 
  delay(1000); 
  mySerial.print("AT+UART="); 
  mySerial.println(uart); 
  Serial.print("Baud rate ayarlandi: "); 
  Serial.println(uart); 
  delay(2000); 
  Serial.println("Islem tamamlandi.");
  Wire.begin();
  Wire.beginTransmission(MPU);
  Wire.write(0x6B);  // PWR_MGMT_1 register
  Wire.write(0);     // set to zero (wakes up the MPU-6050)
  pinMode(trigPin ,OUTPUT);
  pinMode(echoPin, INPUT);
  Wire.endTransmission(true);
}

void loop(){
  Wire.beginTransmission(MPU);
  Wire.write(0x3B);  // starting with register 0x3B (ACCEL_XOUT_H)
  Wire.endTransmission(false);
  Wire.requestFrom(MPU,14,true);  // request a total of 14 registers
  AcX=Wire.read()<<8|Wire.read();  // 0x3B (ACCEL_XOUT_H) & 0x3C (ACCEL_XOUT_L)    
  AcY=Wire.read()<<8|Wire.read();  // 0x3D (ACCEL_YOUT_H) & 0x3E (ACCEL_YOUT_L)
  AcZ=Wire.read()<<8|Wire.read();  // 0x3F (ACCEL_ZOUT_H) & 0x40 (ACCEL_ZOUT_L)
  Tmp=Wire.read()<<8|Wire.read();  // 0x41 (TEMP_OUT_H) & 0x42 (TEMP_OUT_L)
  GyX=Wire.read()<<8|Wire.read();  // 0x43 (GYRO_XOUT_H) & 0x44 (GYRO_XOUT_L)
  GyY=Wire.read()<<8|Wire.read();  // 0x45 (GYRO_YOUT_H) & 0x46 (GYRO_YOUT_L)
  GyZ=Wire.read()<<8|Wire.read();  // 0x47 (GYRO_ZOUT_H) & 0x48 (GYRO_ZOUT_L)
 
 
  mySerial.print(GyX); mySerial.print(";"); mySerial.print(GyY); mySerial.print(";"); mySerial.print(GyZ); mySerial.println(";");
  int data = GetUltra(trigPin, echoPin);
  mySerial.write( data );
  Serial.flush();
  
  delay(15);
}

double GetUltra (int trig, int echo){
    digitalWrite(trigPin,LOW);
    delayMicroseconds(2);
    digitalWrite(trigPin, HIGH);
    delayMicroseconds(8);
    digitalWrite(trigPin,LOW);
    double distance = ((pulseIn(echoPin, HIGH))/2)*0.0343;
    return distance;
    
//    duration = pulseIn(echoPin, HIGH);
 //   distance = (duration / 2) * 0.0343;
   // Serial.print("Distance = ");
 //   if (distance >= 4000 || distance <= 0){
//      Serial.println("Out of range !!");
 //   }
  //  else{
   //   Serial.print(distance);
   //   Serial.println(" cm");
 //     delay(5);
 //   }
 
 }






    
