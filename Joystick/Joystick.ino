//deklarasi horizontal vertical dan button joystick
int VRx_1 = A0;
int VRy_1 = A1;

int VRx_2 = A2;
int VRy_2 = A3;

int PB_1 = 2;
int PB_2 = 3;

unsigned long prevMillis = 0;
int msDelay = 20;

//fungsi yang dijalankan satu kali
void setup() {
  //deklarasi baud rate yang digunakan
  Serial.begin(19200);

  //setup pin dari joystick 
  pinMode(VRx_1, INPUT);
  pinMode(VRy_1, INPUT);

  pinMode(VRx_2, INPUT);
  pinMode(VRy_2, INPUT);

  pinMode(PB_1, INPUT_PULLUP);
  pinMode(PB_2, INPUT_PULLUP);
}

void loop(){
  unsigned long currentMillis = millis();
  if(currentMillis - prevMillis > msDelay){
    inputLoop();
    
    prevMillis = currentMillis;
  }
}

// fungsi yang dijalankan berulang kali
void inputLoop() {
  //membaca perubahan data dari joystick 1
  int xPosition_1 = analogRead(VRx_1);
  int yPosition_1 = analogRead(VRy_1);

  int mapX_1 = map(xPosition_1, 1, 1024, -4, 6);
  int mapY_1 = map(yPosition_1, 1, 1024, -5, 5);

  if (mapX_1 > 0)
    mapX_1 = 1;
  if (mapY_1 > 0)
    mapY_1 = 1;

  if (mapX_1 < 0)
    mapX_1 = -1;
  if (mapY_1 < 0)
    mapY_1 = -1;

  //membaca perubahan data dari joystick 2
  int xPosition_2 = analogRead(VRx_2);
  int yPosition_2 = analogRead(VRy_2);

  int mapX_2 = map(xPosition_2, 1, 1024, -5, 5);
  int mapY_2 = map(yPosition_2, 1, 1024, -5, 5);

  if (mapX_2 > 0)
    mapX_2 = 1;
  if (mapY_2 > 0)
    mapY_2 = 1;

  if (mapX_2 < 0)
    mapX_2 = -1;
  if (mapY_2 < 0)
    mapY_2 = -1;

  //membaca perubahan data dari pushbutton
  int PB_state_1 = digitalRead(PB_1);
  int PB_state_2 = digitalRead(PB_2);
  
  //mencetak perubahan data yang terjadi
  //joystick-1
  Serial.print(mapX_1);
  Serial.print(",");
  Serial.print(mapY_1);
  Serial.print(",");

  //joystick-2
  Serial.print(mapX_2);
  Serial.print(",");
  Serial.print(mapY_2);
  Serial.print(",");

  //pushbutton
  Serial.print(PB_state_1);
  Serial.print(",");
  Serial.println(PB_state_2);
}
