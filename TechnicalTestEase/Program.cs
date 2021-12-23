using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TechnicalTestEase
{
    class Program
    {
        static Dictionary<MountainNode, List<List<MountainNode>>> _routes = new Dictionary<MountainNode, List<List<MountainNode>>>();
        static List<string[]> _stringWorkSpace;
        static int[,] _numericWorkSpace;
        static void Main(string[] args)
        {
            //TODO: Cambiar a relative path
            var pathFileInput = @"C:\Users\harold.salgado\Downloads\Technical test ease\4x4.txt";
            var fileLines = File.ReadLines(pathFileInput).ToList();
            var matrixSize = fileLines.First().Split(' ');
            var numberOfRows = int.Parse(matrixSize.First());
            var numverOfColumns = int.Parse(matrixSize.Last());
            fileLines.RemoveAt(0);
            var matrix = fileLines.Select(l => l.Split(' ')).ToList();
            _stringWorkSpace = new List<string[]>(matrix);
            var numericValues = _stringWorkSpace.Select(l => l.Select(int.Parse).ToArray()).ToList();
            _numericWorkSpace = new int[numberOfRows,numverOfColumns];
            for (int x = 0; x < numverOfColumns; x++)
            {
                for (int y = 0; y < numverOfColumns; y++)
                {
                    _numericWorkSpace[x, y] = numericValues.ElementAt(y).ElementAt(x);
                }
            }

            while (true)
            {
                var indexTop = GetIndexTop();
                var newRoute = new List<MountainNode>();
                newRoute.Add(indexTop);
                ProcessRoutes(newRoute);
                _numericWorkSpace[indexTop.CoordinateX, indexTop.CoordinateY] = int.MinValue;
            }

            //for (int x = 0; x < int.Parse(matrixSize.ElementAt(0)); x++)
            //{
            //    for (int y = 0; y < int.Parse(matrixSize.ElementAt(1)); y++)
            //    {
            //        var currentElement = matrix.ElementAt(x).ElementAt(y);
            //    }
            //}



            //foreach (var column in matrix)
            //    foreach (var altitude in column)
            //    {
            //        Console.WriteLine(altitude);
            //    }
            //{

            //}

            //var matrix = fileLines.ToArray();
            //Console.WriteLine(String.Join(Environment.NewLine, line));
        }

        static List<MountainNode> GetMovements(int x, int y, List<string[]> matrix)
        {
            var result = new List<MountainNode>();
            //var value = matrix.ElementAtOrDefault(x).ElementAtOrDefault(y);

            return result;
        }

        static List<List<MountainNode>> ProcessRoutes(List<MountainNode> currentRoute)
        {
            var currentNode = currentRoute.Last();
            var isExploredNode = _routes.TryGetValue(currentNode, out _);
            var possiblerRoutes = new List<List<MountainNode>>();
            if (isExploredNode)
            {
                possiblerRoutes = _routes[currentNode];
                //result.FirstOrDefault(r=>r.FirstOrDefault(l=>l.CoordinateX == currentNode.CoordinateX && l.CoordinateY == currentNode.CoordinateY));
            }
            else
            {
                ProcessDirection(0, -1, currentRoute);
                //var up = ProcessDirection(0, -1, currentRoute);
                //if (up != null)
                //{
                //    var routeToAdd = new List<MountainNode>(currentRoute);
                //    routeToAdd.Add(up);
                //    possiblerRoutes.Add(routeToAdd);
                //}
                ProcessDirection(0, 1, currentRoute);
                //var down = ProcessDirection(0, 1, currentRoute);
                //if (down != null)
                //{
                //    var routeToAdd = new List<MountainNode>(currentRoute);
                //    routeToAdd.Add(down);
                //    possiblerRoutes.Add(routeToAdd);
                //}
                ProcessDirection(-1, 0, currentRoute);
                //var left = ProcessDirection(-1, 0, currentRoute);
                //if (left != null)
                //{
                //    var routeToAdd = new List<MountainNode>(currentRoute);
                //    routeToAdd.Add(left);
                //    possiblerRoutes.Add(routeToAdd);
                //}
                ProcessDirection(1, 0, currentRoute);
                //var right = ProcessDirection(1, 0, currentRoute);
                //if (right != null)
                //{
                //    var routeToAdd = new List<MountainNode>(currentRoute);
                //    routeToAdd.Add(right);
                //    possiblerRoutes.Add(routeToAdd);
                //}
                //var down = _stringWorkSpace.ElementAtOrDefault(currentNode.CoordinateX).ElementAtOrDefault(currentNode.CoordinateY + 1);
                //var left = _stringWorkSpace.ElementAtOrDefault(currentNode.CoordinateX - 1).ElementAtOrDefault(currentNode.CoordinateY);
                //var right = _stringWorkSpace.ElementAtOrDefault(currentNode.CoordinateX + 1).ElementAtOrDefault(currentNode.CoordinateY);
                _routes.Add(currentNode, possiblerRoutes);
            }
            return possiblerRoutes;
        }

        static MountainNode GetIndexTop()
        {
            var result = new MountainNode();
            var max = _numericWorkSpace.Cast<int>().Max();
            for (int x = 0; x < _numericWorkSpace.GetLength(0); x++)
            {
                for (int y = 0; y < _numericWorkSpace.GetLength(1); y++)
                {
                    var value = _numericWorkSpace[x, y];
                    if (value == max)
                    {
                        result = new MountainNode(x,y,value);
                    }
                }
            }
            return result;
        }

        static void ProcessDirection(int offsetX,int offsetY, List<MountainNode> currentRoute)
        {
            MountainNode result = null;
            var currentNode = currentRoute.Last();
            var up = _stringWorkSpace.ElementAtOrDefault(currentNode.CoordinateY + offsetY)?.ElementAtOrDefault(currentNode.CoordinateX + offsetX);
            //var up = _stringWorkSpace.ElementAtOrDefault(currentNode.CoordinateX + offsetX).ElementAtOrDefault(currentNode.CoordinateY + offsetY);
            //if (up != null && int.Parse(up) < currentNode.Value)
            //{
            //    result = new MountainNode(currentNode.CoordinateX + offsetX, currentNode.CoordinateY + offsetY, int.Parse(up));
            //}
            //return result;
            if (up != null && int.Parse(up) < currentNode.Value)
            {
                var newRoute = new List<MountainNode>(currentRoute);
                newRoute.Add(new MountainNode(currentNode.CoordinateX + offsetX, currentNode.CoordinateY + offsetY, int.Parse(up)));
                //var resultRoutes = ProcessRoutes(newRoute);
                ProcessRoutes(newRoute);
                //if (resultRoutes.Count > 0)
                //{
                //    foreach (var routeFromCurrent in resultRoutes)
                //    {
                //        var copyResultRoute = new List<MountainNode>(currentRoute);
                //        //var routeToAdd = new List<MountainNode>(copyCurrentRoute);
                //        copyResultRoute.AddRange(routeFromCurrent);
                //        result.Add(copyResultRoute);
                //    }
                //}
                //else
                //{
                //    result.Add(currentNode);
                //}
                //var copyCurrent = new List<MountainNode>(currentRoute);
                //copyCurrent.Add(new MountainNode(currentNode.CoordinateX, currentNode.CoordinateY, currentNode.Value));
                //possiblerRoutes.Add(copyCurrent);

            }
            //return result;
        }

        //MountainNode GetTop()
        //{
        //    _numericWorkSpace
    }
    class MountainNode
    {
        public int CoordinateX { get; set; }
        public int CoordinateY { get; set; }
        public int Value { get; set; }
        public MountainNode()
        {

        }

        public MountainNode(int coordinateX, int coordinatey, int value)
        {
            this.CoordinateX = coordinateX;
            this.CoordinateY = coordinatey;
            this.Value = value;
        }
    }
}

