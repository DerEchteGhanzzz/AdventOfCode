using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode
{
    public class Day_20
    {
        private static Parser p = new Parser("20");
        private static string algorithm;
        public static int solve_A()
        {
            var lines = p.Lines;
            algorithm = lines[0].Replace("#", "1").Replace(".", "0");
            
            string[] image = ParseInput(lines);

            for (int step = 0; step < 50; step++)
            {
                
                image = EnlargeField(image, step%2);
                image = Enhance(image);
                
            }
            
            int count = 0;
            for (int y = 0; y < image.Length; y++)
            {
                for (int x = 0; x < image[0].Length; x++)
                {
                    if (image[y][x] == '1')
                    {
                        count++;
                    }
                }
            }

            return count;
        }
        
        public static int solve_B()
        {
            var lines = p.Lines;

            return 0;
        }


        private static string[] Enhance(string[] image)
        {
            string[] enhancedImage = new string[image.Length-2];
            // enhancedImage[0] = image[0].Substring(0,image[0].Length);
            // enhancedImage[enhancedImage.Length - 1] = image[0].Substring(0,image[0].Length);
            for (int y = 1; y < image.Length-1; y++)
            {
                string line = "";
                for (int x = 1; x < image[0].Length-1; x++)
                {
                    string bitString = image[y - 1].Substring(x - 1, 3) +
                                       image[y].Substring(x - 1, 3) +
                                       image[y + 1].Substring(x - 1, 3);
                    // Console.WriteLine(bitString);
                    
                    int index = Convert.ToInt32(bitString, 2);
                    char targetChar = algorithm[index];
                    line = line + targetChar;
                    
                }
                
                enhancedImage[y-1] = line;
            }
            
            return enhancedImage;
        }


        private static string[] EnlargeField(string[] image, int even)
        {
            string[] largeImage = new string[image.Length + 4];
            string zeros = "";
            if (even == 0)
            {
                zeros = "0000";
            }
            else
            {
                zeros = "1111"; 
            }
            
            for (int i = 0; i < image[0].Length; i++)
            {
                zeros = zeros + zeros[0];
            }
            
            for (int i = 0; i < 2; i++)
            {
                largeImage[i] = zeros;
            }
            
            for (int i = 2; i < image.Length+2; i++)
            {
                largeImage[i] = zeros.Substring(0, 2) + image[i - 2] + zeros.Substring(0, 2);
                
            }
            
            for (int i = image.Length+2; i < image.Length+2*2; i++)
            {
                largeImage[i] = zeros;
            }
            
            return largeImage;
        }

        private static string[] ParseInput(string[] lines)
        {
            string[] image = new string[lines.Length - 2];
            for (int i = 2; i < lines.Length; i++)
            {
                string bitString = lines[i].Replace('#', '1').Replace('.', '0');
                image[i-2] = bitString;
            }

            return image;
        }
    }
}