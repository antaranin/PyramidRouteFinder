# PyramidRouteFinder

PyramidRouteFinder is a simple library that allows finding longest (largest) 
paths in pyramid shaped integer data.
The project was created as a coding exercise in writing maintainable C# algorithms.

## Setup

I recommend using JetBrains Rider in order to fully take advantage 
of type annotations in the project, however can also be simply run with Visual Studio.

## Usage

```C#
using PyramidRouteFinderLib;
…

        static void Main(string[] args)
        {
            const string path = "some/path.txt";
            //Instantiate
            var finder = new RouteFinder();
            //Extract route
            var route = finder.FindNumericalRouteFromFile(path);

        }

```

## License
Copyright [2019] [Rafal Markiewicz]

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.