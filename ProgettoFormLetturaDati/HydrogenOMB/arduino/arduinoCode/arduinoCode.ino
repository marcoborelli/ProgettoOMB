const byte buttonStart = 2;
const byte buttonStop = 3;
bool previousStateBS = false;
bool previousStateBF = false;

byte bStop = 0;
bool lettura = false;
byte loops = 0;
int delta = 1;

void setup() {
  Serial.begin(9600);
}

void loop() {

  if (!lettura && digitalRead(buttonStart) == HIGH && !previousStateBS) {
    Serial.println("start");
    lettura = true;
    loops = 0;
  }

  if (lettura) {
    if (loops == 100) {
      Serial.println("endOpen");
      delta = -1;
    } else {
      Serial.print(loops);
      Serial.print(";");
      Serial.println(analogRead(A1));
    }
    loops+=delta; /*per ora simulo che ogni mezzo sec giro di 1 grado*/

    delay(250);
    if (loops <= 0) {
      lettura = false;
      Serial.println("stop");
    } else if (digitalRead(buttonStop) == HIGH) {
      lettura = false;
      Serial.println("fstop");
    }
  }
  previousStateBS = digitalRead(buttonStart);
}