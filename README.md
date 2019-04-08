﻿# Median Finder

![GitHub release](https://img.shields.io/github/release/singhrahulnet/medianfinder.svg?style=for-the-badge) ![Maintenance](https://img.shields.io/maintenance/yes/2019.svg?style=for-the-badge)

![GitHub Release Date](https://img.shields.io/github/release-date/singhrahulnet/medianfinder.svg?style=plastic) [![.Net Framework](https://img.shields.io/badge/DotNet-Framework_2.1-blue.svg?style=plastic)](https://www.microsoft.com/net/download/dotnet-core/2.1)  ![GitHub language count](https://img.shields.io/github/languages/count/singhrahulnet/medianfinder.svg?style=plastic) ![GitHub top language](https://img.shields.io/github/languages/top/singhrahulnet/medianfinder.svg) 


###### A .NET Core console application to 
> Read flat files from configured path 

> Determine data column name based on various file types configured and

> Find median variance by percentage per file


### Setup Details

#### Environment Setup

> Download/install [![.Net Framework](https://img.shields.io/badge/DotNet-Framework_2.1-blue.svg?style=plastic)](https://www.microsoft.com/net/download/dotnet-core/2.1) to run the project   
 

> Download/clone the repository.


#### Running the Application
> Open solution file with Visual Studio

> Set source folder path in appsettings.json
<img width="500" src="https://github.com/singhrahulnet/MedianFinder/blob/master/refImg/config.PNG">

> Hit F5 from within Visual Studio

> The output can be viewed in the console window


### Problem Statement

There are two specific types of CSV files – so-called "LP" and "TOU"
files. Write a console program that will:
1. Read CSV files, set the file path configurable so the program can read any "LP" and
"TOU" files;
2. For each file, calculate the median value using a) the "Data Value" column for the
"LP" file type or b) or the "Energy" column for the "TOU" file type;
3. Find values that are 20% above or below the median, and print to the console using
the following format:
{file name} {datetime} {value} {median value}
Note: to get {datetime} use "Date/Time" column in a csv file (for both file
types).

### Solution
The console application reads all the configured valid files from a configured path and an output service prints all the variances in a console. Apart from file types, file extension/delimiter/upper and lower variance bounds are also configurable in the appsettings.json.

#### Design Highlights
##### Adding a New File Type
    In order to include more file types (e.g. LP or TOU), simply add the setting to appsettings.json.

<img width="500" src="https://github.com/singhrahulnet/MedianFinder/blob/master/refImg/AddNewFileType.PNG">

##### S.O.L.I.D Principles
    The software design is lucid, extensible and maintainable by adhering to S.O.L.I.D principles.

##### Inversion of Control
    The dependencies are inverted using IoC container.

##### Unit and Integration Tests
    The tests are written to make the software robust. Include the test cases in CI/CD pipeline.

#### Class Diagram

<img width="1469" src="https://github.com/singhrahulnet/MedianFinder/blob/master/refImg/ClassDiagram.PNG">


### Support or Contact
Having any trouble? Please read out this [documentation](https://github.com/singhrahulnet/medianfinder/blob/master/README.md) or [contact](mailto:singh.rahul.net@gmail.com) to sort it out.

[![HitCount](http://hits.dwyl.io/singhrahulnet/lms/projects/1.svg)](http://hits.dwyl.io/singhrahulnet/lms.api/projects/1)  ![GitHub contributors](https://img.shields.io/github/contributors/singhrahulnet/lms.api.svg?style=plastic)
 
 
 
Keep Coding :-) 
