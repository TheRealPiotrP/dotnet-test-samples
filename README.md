# dotnet-test-samples
A fairly trivial sample project to exhibit basic usage of `dotnet test` with the `dotnet-test-xunit` runner. Note, this runner is still a prototype and it has not yet been approved by the good folks driving the xunit project.

# Changes Explained
This project differs from a stock `dotnet new` project as described here:

## NuGet.config
NuGet.config references two additional feeds:

* https://www.myget.org/F/coreclr-xunit/api/v2
This feed hosts the prototype xuntit runner. As mentioned above, this is a prototype feed and you should wait until an official runner is available from the xunit project before using this in production.

* https://www.myget.org/F/dotnet-cli
This feed hosts unofficial packages from the `dotnet cli` repo. These are dependencies of the prototype xunit runner.

## project.json
The project.json has been modified for use as a test project:
````
{
    "version": "1.0.0-*",

    "dependencies": {
      "NETStandard.Library": "1.0.0-rc2-23714",
		  "xunit": "2.1.0",        
      "dotnet-test-xunit": "1.0.0-dev-*"
    },

    "frameworks": {
        "dnxcore50": { }
    },
    
	"testRunner":"xunit"
}
````

* `compilerOptions` has been removed as this is now a library project.
* `dependencies have` been updated
  * `xunit` was added to add `FactAttribute`, etc.
  * `dotnet-test-xunit` was added. This is the runner itself.
* `testRunner` property was added to tell `dotnet-test` which runner to invoke.

# Why is dotnet-test-xunit not a tool dependency
Dotnet CLI supports a tools list in the project.json where folks can specify additional tools to use in their application. Why, then, is the xunit runner listed as a dependency? Test runners like xunit end up loading test libraries in-process for execution. To enable this scenario, the dependencies of the runner must be coalessed with those of the test library. This class of tools is therefore referenced as a `dependency` to allow NuGet to figure out the most appropriate dependency versions to use for both the runner and the library under test.

The `tools` listing in project.json is used for tools that do not load the target library for execution and can therefore have their dependencies resolved without taking the project itself into account.
