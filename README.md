# Demo UDP Application

This application is written in C#, uses Windows Forms, and implements a UDP client that can both send and receive UDP messages. Message types are represented as objects in C# with appropriate polymorphism. Messages offer Parse and ToBytes methods for converting from/to binary data that can be sent via UDP.

## Testing the app

1. Run the program
2. By default the program will send messages to your local machine on the listening port, making it a simple loopback communication. Try sending a few messages and observe the log.
3. On another PC on the same network, you can run the same application and then use that machine's IP address and the randomly selected UDP port. Fill in those values into the first PC and try sending messages to another machine.

## Exploring the code

The main form uses many fundamental WinForms components.

The UdpMessage code file contains both an abstract class as well as several classes that inherit from the abstract class. Recall that abstract classes can't be directly instantiated, so all instances of `DemoUdpMessage` are actually instances of one of its subclasses.

The static method on `DemoUdpMessage` allows you to parse a byte array into one of the subclasses. It is valid to have *static* methods on abstract classes, since static methods do not require instantiation.

The actual UDP client code is in the main form's code file.
