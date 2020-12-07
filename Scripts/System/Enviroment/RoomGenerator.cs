using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp19
{
    public static class RoomGenerator
    {
        static int[,] arr;
        static int n;
        static int startX;
        static int startY;

        static int minN = 5;
        static int maxN;

        static int maxHeight=0;
        static int[,] tempArr;

        public static int roomSymbol = 1;

        public static int[,] GetRooms(int size)
        {
            n = size;
            if (n <= minN * 3)
            {
                throw new ArgumentException($"Provided n = {n} is not suitable. Please fill value > 15");
            }
            maxN = n / 3;
            arr = new int[n, n];

            int width = 0, height = 0;
            startX = 0; startY = 0;
            Random rand = new Random(DateTime.Now.Millisecond);

            //first floor
            //initial room
            width = rand.Next(minN, maxN);
            height = rand.Next(minN, maxN);
            SetHeight(height);

            FillRoom(width, height);
            startX += width;

            //other rooms
            while (n - startX > minN)
            {
                do
                {
                    width = rand.Next(minN, maxN);
                    height = rand.Next(minN, maxN);
                } while (startX + width - 1 >= n);
                if (IsAccessibleToLeft(width, height))
                {
                    SetHeight(height);
                    startX -= 1;
                    FillRoom(width, height);

                    startX += width;
                }
            }
            AddDoor();
            CopyFirstFloor();

            //second floor of labirinth
            //initial room
            startX = 0;
            startY = maxHeight;

            width = rand.Next(minN, maxN);
            height = rand.Next(minN, maxN);
            //SetHeight(height);

            FillRoom(width, height);
            startX += width;
            //other rooms
            while (n - startX > minN)
            {
                do
                {
                    width = rand.Next(minN, maxN);
                    height = rand.Next(minN, maxN);
                } while (startX + width - 1 >= n);
                if (IsAccessibleToLeft(width, height))
                {
                    //SetHeight(height);
                    startX -= 1;
                    FillRoom(width, height);

                    startX += width;
                }
            }
            AddDoor();
            RevertFirstFloor();

            return arr;
        }

        private static void ShowM(int[,] arr, int n)
        {
            for (int i = n - 1; i >= 0; --i)
            {
                for (int j = 0; j < n; ++j)
                {
                    Console.Write($"{arr[i, j]} ");
                }
                Console.WriteLine();
            }
        }

        private static void FillRoom(int roomWidth, int roomHeight)
        {

            for (int i = startY; i < startY + roomHeight; ++i)
            {
                arr[i, startX] = roomSymbol;
            }
            for (int i = startX; i < startX + roomWidth; ++i)
            {
                arr[startY + roomHeight - 1, i] = roomSymbol;
            }
            for (int i = roomHeight + startY - 1; i > startY; --i)
            {
                arr[i, startX + roomWidth - 1] = roomSymbol;
            }
            for (int i = startX + roomWidth - 1; i > startX; --i)
            {
                arr[startY, i] = roomSymbol;
            }
        }

        private static bool IsAccessibleToLeft(int roomWidth, int roomHeight)
        {
            for (int i = startY; i < startY + roomHeight; ++i)
            {
                if (arr[i, startX] == roomSymbol)
                    return false;
            }
            for (int i = startX; i < startX + roomWidth; ++i)
            {
                if (arr[startY + roomHeight - 1, i] == roomSymbol)
                    return false;
            }
            for (int i = roomHeight + startY - 1; i > startY; --i)
            {
                if (arr[i, startX + roomWidth - 1] == roomSymbol)
                    return false;
            }
            for (int i = startX + roomWidth - 1; i > startX; --i)
            {
                if (arr[startY, i] == roomSymbol)
                    return false;
            }

            return true;
        }

        private static void AddDoor(bool hasExit = false)
        {
            Random r = new Random(DateTime.Now.Millisecond);
            int y = r.Next(startY + 1, startY + 4);
            int lastX = 0, lastY = 0;
            for (int i = 1; i < n; ++i)
            {
                if (arr[y, i] == roomSymbol)
                {
                    arr[y, i] = 0;
                    lastX = i;
                    lastY = y;
                    y = r.Next(startY + 1, startY + 3);
                }
            }
            if (!hasExit)
            {
                arr[lastY, lastX] = roomSymbol;
            }
        }

        private static void SetHeight(int h)
        {
            if (h > maxHeight)
                maxHeight = h;
        }

        private static void CopyFirstFloor()
        {
            tempArr = new int[maxHeight, n];
            for(int i=0; i<maxHeight; ++i)
            {
                for(int j=0; j<n; ++j)
                {
                    tempArr[i, j] = arr[maxHeight -1 - i, j];
                }
            }
        }

        private static void RevertFirstFloor()
        {
            for (int i = 0; i < maxHeight; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    if (i == 0)
                        arr[i, j] = 0;
                    arr[i+1, j] = tempArr[i, j];
                }
            }

            //add door
            Random r = new Random(DateTime.Now.Millisecond);
            int rand = r.Next(1, 4);
            arr[maxHeight, rand] = 0;

            //shift rooms to the center
            int temp = 0;
            for(int j = 0; j < n; ++j)
            {
                for(int i = n - maxN; i >= 0; --i)
                {
                    temp = arr[i + maxN - 1, j];
                    arr[i + maxN - 1, j] = arr[i, j];
                    arr[i, j] = temp;
                }
            }

            //fill field with 2
            for(int j=0; j<n; ++j)
            {
                for(int i = 0; i < n; ++i)
                {
                    if (arr[i, j] == 0)
                        arr[i, j] = 2;
                    if (arr[i, j] == 1)
                        break;
                }
            }
            for (int j = 0; j < n; ++j)
            {
                for (int i = n-1; i>=0; --i)
                {
                    if (arr[i, j] == 0)
                        arr[i, j] = 2;
                    if (arr[i, j] == 1)
                        break;
                }
            }
        }

    }
}
