using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Reader;
using Utilities;


public class PathFinding : MonoBehaviour
{
    int[] dr = { -1, -1, -1, 0, 0, 1, 1, 1 }, dc = { -1, 0, 1, -1, 1, -1, 0, 1 };
    double[,] latitudeData, longitudeData, slopeData, heightData;
    UtilFunctions util = new UtilFunctions();
    class Node
    {
        public int r, c;
        public double g, h;
        public Node(int R, int C, double G, double H)
        {
            r = R;
            c = C;
            g = G;
            h = H;
        }
    }

    class NodeComparer : IComparer<Node>
    {
        public int Compare(Node first, Node second)
        {
            if (first.g + first.h < second.g + second.h) return -1;
            if (first.g + first.h == second.g + second.h) return 0;
            return 1;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CSVReader read = new CSVReader();
        read.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Returns the length of the shortest path from a[r1][c1] to a[r2][c2]
    void AStar(int r1, int c1, int r2, int c2)
    {
        double[,] minF = new double[1277, 1277];
        PriorityQueue<Node, Node> open = new PriorityQueue<Node, Node>(new NodeComparer());
        double h = computeDiagonalDist(r1, c1, r2, c2);
        open.Enqueue(new Node(r1, c1, 0, h), h);
        minF[r1, c1] = h;
        PriorityQueue<Node, Node> closed = new PriorityQueue<Node, Node>(new NodeComparer());
        while (open.Count > 0)
        {
            Node top = open.Dequeue();
            if (top.r == r2 && top.c == c2)
            {
                // retrace path
                return;
            }
            for (int i = 0; i < 8; i++)
            {
                if (inBounds(top.r+dr[i], top.c+dc[i]) && slopeData[top.r + dr[i], top.c + dc[i]] <= 15)
                {
                    // to change type of optimization just change it to changeInHeightOrZero
                    double gSuccessor = top.g + computePythagoreanDist(top.r, top.c, top.r + dr[i], top.c + dc[i]);
                    double hSuccessor = computeDiagonalDist(top.r + dr[i], top.c + dc[i], r2, c2);
                    if (!(minF[top.r + dr[i], top.c + dc[i]] > 0 && minF[top.r + dr[i], top.c + dc[i]] < gSuccessor + hSuccessor)) {
                        open.Enqueue(new Node(top.r + dr[i], top.c + dc[i], gSuccessor, hSuccessor), gSuccessor + hSuccessor);
                        minF[top.r + dr[i], top.c + dc[i]] = gSuccessor + hSuccessor;
                    }
                }
            }
            closed.Enqueue(top);
        }
    }

    // heuristic - pythagorean distance
    double computePythagoreanDist(int r1, int c1, int r2, int c2)
    {
        double[] firstXYZ, secondXYZ;
        firstXYZ = new double[]{ util.getX(latitudeData[r1,c1], longitudeData[r1,c1], heightData[r1,c1]),
                util.getY(latitudeData[r1, c1], longitudeData[r1, c1], heightData[r1, c1]),
                util.getZ(latitudeData[r1, c1], heightData[r1, c1])};
        secondXYZ = new double[]{
                util.getX(latitudeData[r2, c2], longitudeData[r2, c2], heightData[r2, c2]),
                util.getY(latitudeData[r2, c2], longitudeData[r2, c2], heightData[r2, c2]),
                util.getZ(latitudeData[r2, c2], heightData[r2, c2])};
        return util.rangeAB(util.coordinateDifference(firstXYZ, secondXYZ));
    }

    double changeInHeightOrZero(int r1, int c1, int r2, int c2)
    {
        double changeInHeight = heightData[r2, c2] - heightData[r1, c1];
        if (changeInHeight < 0) return 0;
        return changeInHeight;
    }
    double computeDiagonalDist(int r1, int c1, int r2, int c2)
    {
        double firstX = longitudeData[r1, c1], firstY = latitudeData[r1,c1];
        double secondX = longitudeData[r2, c2], secondY = latitudeData[r2, c2];
        return Math.Max(Math.Abs(secondX - firstX), Math.Abs(secondY - firstY));
    }
    bool inBounds(int r, int c)
    {
        return r >= 0 && r < 1277 && c >= 0 && c < 1277;
    }
}
