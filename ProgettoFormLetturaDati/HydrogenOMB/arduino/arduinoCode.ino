/*const int pinLedROSSO = 2;
const int pinLedBLU = 3;*/
int uwu=0;
void setup() {
  /*pinMode(pinLedROSSO, OUTPUT);
  pinMode(pinLedBLU, OUTPUT);*/
  Serial.begin(9600);
}

void loop() {
  /*if(uwu > 50) {
    Serial.println("STOP");
    Serial.end();
  } else {*/
    Serial.print(analogRead(A1));
    Serial.print(";");
    Serial.println(analogRead(A0));
  /*}*/
  delay(500);
  uwu++;
}
