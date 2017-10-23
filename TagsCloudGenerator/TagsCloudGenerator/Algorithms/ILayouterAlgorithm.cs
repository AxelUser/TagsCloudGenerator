using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudGenerator.Algorithms
{
    public interface ILayouterAlgorithm
    {
        Rectangle FindSpaceForRectangle(Point center, List<Rectangle> existingRectangles, Size rectangleSize);
    }
}
