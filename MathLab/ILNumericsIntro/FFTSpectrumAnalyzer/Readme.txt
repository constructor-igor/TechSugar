This Examples demonstrates the following features of ILNumerics: 

* Creating a Computing Module
* Importing sound data from an NAudio device 
* Processing the incoming data in realtime
* Calculating the FFT, magnitudes, maximum values 
* Plotting the magnitudes as loglog line plot 
* Marking special values (maximums) in the plot
* Labeling of axis main labels
* dynamic updates to plot objects

INSTALL
=======

The project depends on the NAudio helper library, which is available on nuget. Depending on the 
setting of your Visual Studio installation NAudio will be loaded and installed at the time of 
first run automatically. If not, use the nuget package manager in order to install NAudio 
manually into the example project.

Replace the existing reference to ILNumerics with the one installed on your system: 1) Remove the 
existing reference from the list of References in the project. 2) Add a reference to ILNumerics 
via "Add Reference" -> Select "Assemblies" - "Extensions" and search for ILNumerics. Select ILNumerics 
and click OK to close the window.

Press F5 to compile and run the program. 

Documentation
=============

The online documentation for ILNumerics is available here: 

http://ilnumerics.net/docs.html
http://ilnumerics.net/Getting-Started-with-ILNumerics.html

In case of questions, problems or general feedback: 
http://ilnumerics.net/support.html
