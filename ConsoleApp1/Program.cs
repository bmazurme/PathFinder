using System;

namespace ConsoleApp1
{
    class Program
    {
        private static int MinDistance(int[] distance, bool[] shortestPathTreeSet, int verticesCount)
        {
            int min = int.MaxValue;
            int minIndex = 0;

            for (int v = 0; v < verticesCount; ++v)
            {
                if (!shortestPathTreeSet[v] && distance[v] <= min)
                {
                    min = distance[v];
                    minIndex = v;
                }
            }
            return minIndex;
        }

        private static void Print(int[] distance, int verticesCount, string[] path)
        {
            Console.WriteLine("Point     Distance        Path");

            for (int i = 0; i < verticesCount; ++i)
                Console.WriteLine("{0}\t  {1}\t\t  {2}", i, distance[i], path[i]);
        }

        public static void DijkstraAlgo(int[,] graph, int source, int verticesCount)
        {
            string[] path = new string[verticesCount];
            int[] distance = new int[verticesCount];
            bool[] shortestPathTreeSet = new bool[verticesCount];

            for (int i = 0; i < verticesCount; ++i)
            {
                distance[i] = int.MaxValue;
                shortestPathTreeSet[i] = false;
            }

            distance[source] = 0;

            for (int count = 0; count < verticesCount - 1; ++count)
            {

                int u = MinDistance(distance, shortestPathTreeSet, verticesCount);
                shortestPathTreeSet[u] = true;

                for (int v = 0; v < verticesCount; ++v)
                    if (!shortestPathTreeSet[v] && Convert.ToBoolean(graph[u, v]) 
                        && distance[u] != int.MaxValue && distance[u] + graph[u, v] < distance[v])
                    {
                        distance[v] = distance[u] + graph[u, v];
                        path[v] = v + "<" + u;
                    }
            }

            for (int i = 0; i < path.Length; i++)
            {
                if (path[i] != null)
                    for (int j = 0; j < path.Length; j++)
                    {
                        if (path[j] != null)
                            if (path[j].Substring(0, path[j].IndexOf("<")) == path[i].Substring(path[i].LastIndexOf("<")+1))
                            path[i] = path[i] + path[j].Substring(path[j].IndexOf("<"));
                    }
            }

            Print(distance, verticesCount, path);
        }

        static void Main(string[] args)
        {
            int[,] graph =  {
                                { 0, 6,  0, 0,  0,  0,  0, 9,  0, 0,  0},
                                { 6, 0,  9, 0,  0,  0,  0, 11, 0, 0,  0},
                                { 0, 9,  0, 5,  0,  6,  0, 0,  2, 0,  0},
                                { 0, 0,  5, 0,  9,  16, 0, 0,  0, 0 , 0},
                                { 0, 0,  0, 9,  0,  10, 0, 0,  0, 15, 0},
                                { 0, 0,  6, 0,  10, 0,  2, 0,  0, 0,  0},
                                { 0, 0,  0, 16, 0,  2,  0, 1,  6, 0,  0},
                                { 9, 11, 0, 0,  0,  0,  1, 0,  5, 0,  0},
                                { 0, 0,  2, 0,  0,  0,  6, 5,  0, 0,  0},
                                { 0, 0,  0, 0,  15, 0,  0, 0,  0, 0,  7},
                                { 0, 0,  0, 0,  0,  0,  0, 0,  0, 7,  0}
                            };

            DijkstraAlgo(graph, 0, 11);
            Console.ReadKey();
        }
    }
}