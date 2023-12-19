const byte buttonStart = 2;
const byte buttonStop = 3;
const int del = 0;
int loops = 0;

int arrayOpen[100], arrayClose[100];
bool endOpen = false, endTotal = false;

bool previousStateBS = false;
bool lettura = false;

void setup() {
  Serial.begin(9600);
}

void loop() {

  if (!lettura && digitalRead(buttonStart) == HIGH && !previousStateBS /*così lo si prende solo sul fronte di salita*/) {
    Serial.println("start");
    lettura = true;
    loops = 0;
  }

  if (lettura) {
    if (loops == 100) {
      if (!endOpen) {
        Serial.println("endOpen");
        endOpen = true;
        loops = -1; /*perchè poi lo riaumento subito e lo riporto a 0*/
      } else {
        endTotal = true;
        loops = 0;
      }
    } else if (!endOpen) {
      arrayOpen[loops] = analogRead(A1);
    } else {
      arrayClose[loops] = analogRead(A1);
    }

    loops += 1; /*per ora simulo che ogni mezzo sec giro di 1 grado*/

    delay(100);
    if (endTotal) {
      lettura = false;
      Serial.println("stop");

      delay(250);

      StampaArray(arrayOpen, 100, del, true); /*qui metto come dimensione massima 100 perchè la misurazione si è conclusa del tutto, quindi sono sicuro di avere i 100 valori*/
      Serial.println("EndArrOpen");
      StampaArray(arrayClose, 100, del, false);
      Serial.println("EndArrClose");

      ResetVariabili();
    } else if (digitalRead(buttonStop) == HIGH) {
      lettura = false;
      Serial.println("fstop");

      delay(250);

      if (endOpen) { /*se almeno ho finito l'apertura*/
        StampaArray(arrayOpen, 100, del, true);
        Serial.println("EndArrOpen");
        StampaArray(arrayClose, loops, del, false);
        Serial.println("EndArrClose");
      } else {
        StampaArray(arrayOpen, loops, del, true); /*se non ho finito l'apertura stampo il punto fino a dove sono arrivato e non stampo nemmeno la chiusura*/
        Serial.println("EndArrOpen");
        Serial.println("EndArrClose");
      }
      ResetVariabili();
    }
  }

  previousStateBS = digitalRead(buttonStart);
}

void StampaArray(int arrayCarino[], byte lunghezzaMax, int del, bool apertura) {
  for (byte i = 0; i < 100 && i < lunghezzaMax; i++) {
    apertura ? Serial.print(i) : Serial.print(99 - i);
    Serial.print(";");
    Serial.println(arrayCarino[i]);
    delay(del);
  }
}

void ResetVariabili() {
  loops = 0;
  endOpen = endTotal = false;

  previousStateBS = false;
  lettura = false;
}