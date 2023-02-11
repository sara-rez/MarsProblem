# MarsProblem

This project is an example of an issue I have encountered with an asp.net application on Linux.

It includes 4 APIs:

1. /Book/MarsOn => When there are 1000 (or fewer) concurrent requests in a Linux container, the problem occurs. It does not happen on Windows containers or on IIS

2. /Book/MarsOnAsync => No problem in any environment

3. /Book/MarsOff => No problem in any environment

4. /Book/MarsOffAsync => No problem in any environment

This application requires SQLServer, with a database named "MarsProblem" and a table called "book" on your machine.
