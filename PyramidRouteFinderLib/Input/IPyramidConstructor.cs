using System.Collections.Generic;
using JetBrains.Annotations;
using PyramidRouteFinderLib.Model;

namespace PyramidRouteFinderLib
{
    internal interface IPyramidConstructor<T>
    {
        /// <summary>
        /// Transforms provided data lines into a Pyramid of equivalent type
        /// Throws exception if input is not formatted like a pyramid (1 element on 1st line, 2 on second and so on)
        /// </summary>
        /// <param name="dataLines">
        /// The input represented as a list of rows of the pyramid, each of which is a list of values on that row
        /// Can be empty.
        /// </param>
        /// <returns>
        /// An option of a pyramid. The option will have value if the input is at least one line long, None otherwise.
        /// </returns>
        [NotNull]
        Option<Pyramid<T>> ConstructPyramidFromDataLines([NotNull, ItemNotNull] IList<IList<T>> dataLines);
    }
}