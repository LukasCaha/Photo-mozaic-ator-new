﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace Photo_mozaic_ator
{
    class StoneGenerator
    {
        public static void GenerateOneTile(int imageNum)
        {
            //load image
            Image face = Image.FromFile(AplicationStatus.workingDirectory + @"pokemon/pokemon " + imageNum + ".png");

            //scale down to 16x16 px
            Bitmap smallFace = (Bitmap)Mozaicator.ResizeImage(face, AplicationStatus.tileSize, AplicationStatus.tileSize);

            //determine single color representing face
            int red = 0, green = 0, blue = 0;
            //transparent images tiles fix
            int blackPixels = 0;
            //fix end
            for (int x = 0; x < AplicationStatus.tileSize; x++)
            {
                for (int y = 0; y < AplicationStatus.tileSize; y++)
                {
                    Color c = smallFace.GetPixel(x, y);
                    if (c.R == 0 && c.G == 0 && c.B == 0) blackPixels++;
                    red += c.R;
                    green += c.G;
                    blue += c.B;
                }
            }
            //normalizing
            int tileSizeSquared = (int)Math.Pow(AplicationStatus.tileSize, 2);
            red /= tileSizeSquared - blackPixels;
            green /= tileSizeSquared - blackPixels;
            blue /= tileSizeSquared - blackPixels;

            //snapping to 8*8*8 color pallete (512 colors)
            red /= AplicationStatus.normalizingFactor;
            red *= AplicationStatus.normalizingFactor;
            green /= AplicationStatus.normalizingFactor;
            green *= AplicationStatus.normalizingFactor;
            blue /= AplicationStatus.normalizingFactor;
            blue *= AplicationStatus.normalizingFactor;

            //save as #RRGGBB.bmp
            string name = "";
            name += (red + 256).ToString("X").Substring(1);
            name += (green + 256).ToString("X").Substring(1);
            name += (blue + 256).ToString("X").Substring(1);
            var i2 = new Bitmap(smallFace);
            string savePath = AplicationStatus.workingDirectory + @"pokemon_tiles/#" + name + ".bmp";
            i2.Save(savePath, ImageFormat.Bmp);
        }
    }
}
