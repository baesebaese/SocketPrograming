using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public static class ThreadAsync
    {
        public static void Run() {
            int num = 0;

            Thread t1 = new Thread(() => {
                for (int i=0; i < 100000; i++) {
                    num++;
                }
            });

            t1.Start();

            Thread t2 = new Thread(() => {
                for (int i = 0; i < 100000; i++) {
                    num++;
                }

            });
            t2.Start();

            t1.Join();
            t2.Join();
            Console.WriteLine(num);   
        }

        public static void ThreadAsyncRun() {
            int num = 0;

            object obj = new object();

            Thread t1 = new Thread(() => {
                for (int i = 0; i < 100000; i++) {

                    try {
                        Monitor.Enter(obj); //동기화할 구간 설정
                        num++;
                    }
                    finally {
                        Monitor.Exit(obj);
                    }
               
                }
            });

            t1.Start();

            Thread t2 = new Thread(() => {
                for (int i = 0; i < 100000; i++) {
                    try {
                        Monitor.Enter(obj); // 하나의 객체가 모니터를 사용하는 동안 다른 객체는 대기한다.
                        num++;
                    }
                    finally {
                        Monitor.Exit(obj);
                    }
                }

            });
            t2.Start();

            t1.Join();
            t2.Join();
            Console.WriteLine(num);

        }
    }
}
