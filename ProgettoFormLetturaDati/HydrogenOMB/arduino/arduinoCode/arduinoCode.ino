void setup() {
  Serial.begin(9600);
}

void loop() {

  Serial.print(analogRead(A1));
  Serial.print(";");
  Serial.println(analogRead(A0));

  delay(500);
}
