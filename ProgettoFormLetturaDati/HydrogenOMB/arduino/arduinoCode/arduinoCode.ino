const byte buttonStart = 2;
const byte buttonStop = 3;

byte bStop = 0;

void setup() {
  Serial.begin(9600);
}

void loop() {

  if (digitalRead(buttonStart) == HIGH) {

    Serial.println("start");

    byte loops = 0;
    do {
      if (loops == 100) {
        Serial.println("endOpen");
        loops--;
      }
      Serial.println(analogRead(A1));
      loops++;/*per ora simulo che ogni mezzo sec giro di 1 grado*/
      delay(500);
    } while (loops < 200 && digitalRead(buttonStop) == LOW);

    (loops < 200) ? Serial.println("fstop") : Serial.println("stop");
  }
}
