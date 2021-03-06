﻿using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudGenerator.Visualization
{
    public interface ILayoutPainter
    {
        Bitmap GetImage(List<Tuple<string, int, Rectangle>> wordContainers, int width, int height);
    }
}
