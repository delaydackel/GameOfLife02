using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife02
{
    class Program
    {
        public static int width { get; set; }
        public static int height { get; set; }
        public static int iterations { get; set; }
        public static List<List<bool>> field { get; set; }
        static void Main(string[] args)
        {
            foreach (var item in args)
            {
                Console.WriteLine(item);
            }
            //setSize(args);
            int tmp;
            Int32.TryParse(args[0], out tmp);
            width = tmp;
            Int32.TryParse(args[1], out tmp);
            height = tmp;
            //setIterations(args);
            Int32.TryParse(args[2], out tmp);
            iterations = tmp;

            //populateField();
            var rand  = new Random();
            List<List<bool>> field = new List<List<bool>>();
            for (int i = 0; i < height; i++)
            {
                List<bool> row = new List<bool>();
                for (int j = 0; j < width; j++)
                {
                    tmp = rand.Next(2);
                    if (tmp==0)
                    {
                        row.Add(false);
                    }
                    else
                    {
                        row.Add(true);
                    }                    
                }
                field.Add(row);
            }
            iterations = 0;
            while (true)  
            {
                iterations++;
                Draw(field);
                field = RunIteration(field);
                System.Threading.Thread.Sleep(1);
                Console.WriteLine("i= " + iterations);
            }
            



            //for (int i = 0; i < iterations; i++)
            //{
            //    Draw(field);
            //    field = RunIteration(field);
            //    System.Threading.Thread.Sleep(100);
            //}
            
            Console.ReadLine();
            return;
        }

        static void Draw(List<List<bool>> field)
        {
            var drawingField = new List<string>();
            //border
            var sb = new StringBuilder();
            for (int i = 0; i < field[0].Count+2; i++)
            {
                sb.Append("+");
            }
            //sb.Append(Environment.NewLine);
            drawingField.Add(sb.ToString());
            sb.Clear();

            //main content
            foreach (var row in field)
            {
                sb.Append("+");
                foreach (var point in row)
                {
                    if (point == true)
                    {
                        sb.Append("X");
                        //Console.Write("X");
                    }
                    else
                    {
                        sb.Append(" ");
                        //Console.Write(" ");
                    }
                    //sb.Append(Environment.NewLine);
                    //Console.Write(Environment.NewLine);

                }
                sb.Append("+");
                drawingField.Add(sb.ToString());
                sb.Clear();
            }

            //border
           
            for (int i = 0; i < field[0].Count+2; i++)
            {
                sb.Append("+");
            }
            //sb.Append(Environment.NewLine);
            drawingField.Add(sb.ToString());
            sb.Clear();

            foreach (var line in drawingField)
            {
                Console.WriteLine(line);
            }
        }

        static List<List<bool>> RunIteration(List<List<bool>> field)
        {
            List<List<bool>> nextField= new List<List<bool>>();
            
/*

           y _x->___________________
           | | -1 -1 | 0 -1 | 1 -1  |
           v |_______|______|_______|
             | -1  0 | 0  0 | 1  0  |
             |_______|______|_______|
             | -1  1 | 0  1 | 1  1  |
             |_______|______|_______|

           
 */          
           
           for (int y = 0; y < field.Count; y++)
            {
                List<bool> line= new List<bool>();
                for (int x = 0; x < field[y].Count; x++)
                {
                    //CountNeighbors();
                    int numberOfNeighbors = 0;
                    //first line
                    if (y==0)
                    {
                        //first line, first element
                        if (x==0)
                        {
                            //middle line
                            //if (field[y][x] == true) //dont count self
                            //{
                            //    numberOfNeighbors++;
                            //}
                            if (field[y][x+1] == true)
                            {
                                numberOfNeighbors++;
                            }
                            if (field[y][field[y].Count-1] == true)
                            {
                                numberOfNeighbors++;
                            }

                            //bottom line
                            if (field[y+1][x] == true)
                            {
                                numberOfNeighbors++;
                            }
                            if (field[y+1][x+1] == true)
                            {
                                numberOfNeighbors++;
                            }
                            if (field[y+1][field[y].Count-1] == true)
                            {
                                numberOfNeighbors++;
                            }

                            //top line, wrapped around
                            if (field[field.Count - 1][x] == true)
                            {
                                numberOfNeighbors++;
                            }
                            if (field[field.Count - 1][x+1] == true)
                            {
                                numberOfNeighbors++;
                            }
                            if (field[field.Count - 1][field[y].Count-1] == true)
                            {
                                numberOfNeighbors++;
                            }

                        }

                        //first line, last element
                        if (x==field[y].Count-1)
                        {
                            if (field[0][0] == true)
                            {
                                numberOfNeighbors++;
                            }
                            if (field[y][x-1] == true)
                            {
                                numberOfNeighbors++;
                            }
                            //if (field[y][x] == true) //dont count self
                            //{
                            //    numberOfNeighbors++;
                            //}


                            if (field[1][0] == true)
                            {
                                numberOfNeighbors++;
                            }
                            if (field[y+1][x-1] == true)
                            {
                                numberOfNeighbors++;
                            }
                            if (field[y+1][x] == true)
                            {
                                numberOfNeighbors++;
                            }

                            if (field[field.Count-1][0] == true)
                            {
                                numberOfNeighbors++;
                            }
                            if (field[field.Count-1][field[y].Count-2] == true)
                            {
                                numberOfNeighbors++;
                            }
                            if (field[field.Count - 1][field[y].Count - 1] == true)
                            {
                                numberOfNeighbors++;
                            }


                        }

                        //rest of first line
                        if (x>0 && (x < field[y].Count - 1))
                        {                           
                            if (field[y][x-1] == true) 
                            {
                                numberOfNeighbors++;
                            }
                            //if (field[y][x] == true) //dont count self
                            //{
                            //    numberOfNeighbors++;
                            //}
                            if (field[y][x+1] == true) 
                            {
                                numberOfNeighbors++;
                            }

                            if (field[y+1][x-1] == true)
                            {
                                numberOfNeighbors++;
                            }
                            if (field[y+1][x] == true)
                            {
                                numberOfNeighbors++;
                            }
                            if (field[y+1][x+1] == true)
                            {
                                numberOfNeighbors++;
                            }

                            if (field[field.Count-1][x-1] == true)
                            {
                                numberOfNeighbors++;
                            }
                            if (field[field.Count - 1][x] == true)
                            {
                                numberOfNeighbors++;
                            }
                            if (field[field.Count-1][x+1] == true)
                            {
                                numberOfNeighbors++;
                            }
                        }
                    }
                    //last line
                    if (y==field.Count-1)
                    {
                        if (x==0)
                        {
                            if (field[0][x] == true)
                            {
                                numberOfNeighbors++;
                            }
                            if (field[0][x+1] == true)
                            {
                                numberOfNeighbors++;
                            }
                            if (field[0][field[y].Count-1] == true)
                            {
                                numberOfNeighbors++;
                            }

                            if (field[y-1][x] == true)
                            {
                                numberOfNeighbors++;
                            }
                            if (field[y-1][x+1] == true)
                            {
                                numberOfNeighbors++;
                            }
                            if (field[y-1][field[y].Count - 1] == true)
                            {
                                numberOfNeighbors++;
                            }

                            if (field[y][x+1] == true)
                            {
                                numberOfNeighbors++;
                            }
                            if (field[field.Count - 1][field[y].Count - 1] == true)
                            {
                                numberOfNeighbors++;
                            }

                        }
                        if (x==field[y].Count)
                        {
                            if (field[0][0] == true)
                            {
                                numberOfNeighbors++;
                            }
                            if (field[0][x-1] == true)
                            {
                                numberOfNeighbors++;
                            }
                            if (field[0][x] == true)
                            {
                                numberOfNeighbors++;
                            }

                            if (field[y-1][0] == true)
                            {
                                numberOfNeighbors++;
                            }
                            if (field[y-1][x-1] == true)
                            {
                                numberOfNeighbors++;
                            }
                            if (field[y-1][x] == true)
                            {
                                numberOfNeighbors++;
                            }

                            if (field[y][0] == true)
                            {
                                numberOfNeighbors++;
                            }
                            if (field[y][x-1] == true)
                            {
                                numberOfNeighbors++;
                            }
                            //if (field[y][x] == true)
                            //{
                            //    numberOfNeighbors++;
                            //}

                        }
                        if (x>0&&(x<field[y].Count-1))
                        {
                            if (field[0][x-1] == true)
                            {
                                numberOfNeighbors++;
                            }
                            if (field[0][x] == true)
                            {
                                numberOfNeighbors++;
                            }
                            if (field[0][x+1] == true)
                            {
                                numberOfNeighbors++;
                            }

                            if (field[y-1][x-1] == true)
                            {
                                numberOfNeighbors++;
                            }
                            if (field[y-1][x] == true)
                            {
                                numberOfNeighbors++;
                            }
                            if (field[y-1][x+1] == true)
                            {
                                numberOfNeighbors++;
                            }

                            if (field[y][x-1] == true)
                            {
                                numberOfNeighbors++;
                            }
                            if (field[y][x+1] == true)
                            {
                                numberOfNeighbors++;
                            }
                        }
                    }

                    if (y>0&&(y<field.Count-1))
                    {
                        if (x==0)
                        {
                            if (field[y-1][x] == true)
                            {
                                numberOfNeighbors++;
                            }
                            if (field[y-1][x+1] == true)
                            {
                                numberOfNeighbors++;
                            }
                            if (field[y-1][field[y].Count-1] == true)
                            {
                                numberOfNeighbors++;
                            }

                            //if (field[y][x] == true) //dont count self
                            //{
                            //    numberOfNeighbors++;
                            //}
                            if (field[y][x+1] == true)
                            {
                                numberOfNeighbors++;
                            }
                            if (field[y][field[y].Count-1] == true)
                            {
                                numberOfNeighbors++;
                            }

                            if (field[y+1][x] == true)
                            {
                                numberOfNeighbors++;
                            }
                            if (field[y+1][x+1] == true)
                            {
                                numberOfNeighbors++;
                            }
                            if (field[y+1][field[y].Count-1] == true)
                            {
                                numberOfNeighbors++;
                            }
                        }
                        if (x==field[y].Count-1)
                        {
                            if (field[y-1][0] == true)
                            {
                                numberOfNeighbors++;
                            }
                            if (field[y-1][x-1] == true)
                            {
                                numberOfNeighbors++;
                            }
                            if (field[y-1][x] == true)
                            {
                                numberOfNeighbors++;
                            }

                            if (field[y][0] == true)
                            {
                                numberOfNeighbors++;
                            }
                            if (field[y][x-1] == true)
                            {
                                numberOfNeighbors++;
                            }

                            if (field[y+1][0] == true)
                            {
                                numberOfNeighbors++;
                            }
                            if (field[y+1][x-1] == true)
                            {
                                numberOfNeighbors++;
                            }
                            if (field[y+1][x] == true)
                            {
                                numberOfNeighbors++;
                            }
                        }
                        if (x>0&&(x<field[y].Count-1))
                        {
                            if (field[y - 1][x - 1] == true)
                            {
                                numberOfNeighbors++;
                            }
                            if (field[y][x - 1] == true)
                            {
                                numberOfNeighbors++;
                            }
                            if (field[y + 1][x - 1] == true)
                            {
                                numberOfNeighbors++;
                            }

                            if (field[y - 1][x] == true)
                            {
                                numberOfNeighbors++;
                            }
                            //if (field[i][j] == true)
                            //{
                            //    numberOfNeighbors++;  CENTER DOESNT COUNT
                            //}
                            if (field[y + 1][x] == true)
                            {
                                numberOfNeighbors++;
                            }


                            if (field[y - 1][x + 1] == true)
                            {
                                numberOfNeighbors++;
                            }
                            if (field[y][x + 1] == true)
                            {
                                numberOfNeighbors++;
                            }
                            if (field[y + 1][x + 1] == true)
                            {
                                numberOfNeighbors++;
                            }
                        }
                    }

                    
                    //decideLife();
                    if (field[y][x]&&(numberOfNeighbors==2))
                    {
                        line.Add(true);
                    }
                    else if ((numberOfNeighbors>3))
                    {
                        line.Add(false);
                    }
                    else if (numberOfNeighbors==3)
                    {
                        line.Add(true);
                    }
                    else
                    {
                        line.Add(false);
                    }
                   
                    
                }
                nextField.Add(line);
            }
            return nextField;
        }
        
    }
}
