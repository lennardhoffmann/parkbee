What architectures or patterns are you using currently or have worked on recently?
I have worked with the follwoing design patterns:
-Repository pattern
-State pattern
-Singleton pattern
-Flux pattern

What do you think of them and would you want to implement it again?
Repository pattern
Would definitely keep implementing this pattern. It helps create a decint centralised way of handling entity and domain objects

State pattern
Again depending on the needs and complexities of the application. it is useful to alter the behavious of the object based on internal state as 
opposed to initialising new objectsto chandle the new behaviour

Facade pattern
Depending on the applications frameworks and libraries used it is beneficial to use in order to delegate functionality via the interface to classes

Singleton pattern
It would purely depend on the need for the pattern. Certain applications make a singleton object obsolete. 

Flux pattern
This is currently the go-to pattern for UI design. It makes state management easier for components when a centralized state system existss. Would implement again

What version control system do you use or prefer?
I use git for version control

What is your favorite language feature and can you give a short snippet on how you use it?
My favourite feature of c# is the expression body definition. It saves a lot of time and makes coding much easier when having to simply return a single property/method. it also makes for much more readable coding
e.g. public string GetName() => "My name"

What future or current technology do you look forward to the most or want to use and why?
i really look forward to learning a lot more cloud computing principles. I am already familiar with containerization and heve worked with ocker.
I look forward to learning more about setting up container instances such as linux EC/EC2 instances on AWS, Kubernetes or Azure

How would you find a production bug/performance issue? Have you done this before?
I have had to delve into finding production issues. 
The simplest starting point would be to open the newtwork trace to determine which method is causing an issue. After that it would be advisable to step through thecontroller method to see if any sub-methods or services are causing additional issues. 
This will help pinpoint exactly where the issue rises

How would you improve the sample API (bug fixes, security, performance, etc.)?
An additional improvement to the api would be to add a much more detailed and generic exception handling class in order to better redirect to submethods if required as well as to provide a more detailed explanation of the exception to 
further refactor and improve any methods that would cause the exception