using System;

namespace TxtRPG
{
    internal class Program
    {
        enum ClassType   // enum 열거형 타입 설정.
        { None = 0, Knight = 1, Archer = 2, Magician = 3 }

        struct Player   // struct 구조체 선언. (데이터들을 세트로 사용할 수 있음)
        {
            public int hp;
            public int atk;
        }

        static ClassType ChooseClass( )   // Main()에서 호출하도록 static으로 정의, ClassType형식으로 반환함.
        {
            ClassType choice = ClassType.None; // ClassType 초기화.

            Console.WriteLine($"직업을 선택하세요.");

            Console.WriteLine(" [1] Knight");
            Console.WriteLine(" [2] Archer");
            Console.WriteLine(" [3] Magician");

            string input = Console.ReadLine( );

            switch(input)   // 무조건 선택하도록 switch문 사용.
            {
                case "1":
                    choice = ClassType.Knight;
                    break;
                case "2":
                    choice = ClassType.Archer;
                    break;
                case "3":
                    choice = ClassType.Magician;
                    break;
            }
            return choice;
        }

        // out을 활용하여 직접적인 변수에 데이터 입력, struct구조체 Player타입을 매개변수로 받음.
        static void CreatePlayer(ClassType choice, out Player player )
        {
            //기사(100/10), 궁수(75,12), 법사(50/15)
            switch(choice)
            {
                case ClassType.Knight:
                    player.hp = 100;
                    player.atk = 10;
                    break;
                case ClassType.Archer:
                    player.hp = 75;
                    player.atk = 12;
                    break;
                case ClassType.Magician:
                    player.hp = 50;
                    player.atk = 15;
                    break;
                default:
                    player.hp = 0;
                    player.atk = 0;
                    break;
            }

        }


        enum MonsterType   // 몬스터 enum 열거형 타입 설정.
        {
            None = 0, Slime = 1, Orc = 2, Skeleton = 3
        }

        struct Monster   // 몬스터 struct 구조체 선언.
        {
            public int hp;
            public int atk;
        }

        static void CreateRandomMonster(out Monster monster)
        {   
            Random random = new Random( );          // 랜덤 리스트 생성.
            int randomMonster = random.Next(1, 4);  // 정수타입인 randomMonster = 1-3의 정수를 랜덤으로 담음.

            switch(randomMonster)
            {
                case (int)MonsterType.Slime:
                    Console.WriteLine("Slime이 나타났다");
                    monster.hp = 20;
                    monster.atk = 2;
                    break;
                case (int)MonsterType.Orc:
                    Console.WriteLine("Orc이 나타났다");
                    monster.hp = 40;
                    monster.atk = 5;
                    break;
                case (int)MonsterType.Skeleton:
                    Console.WriteLine("Skeleton이 나타났다");
                    monster.hp = 30;
                    monster.atk = 3;
                    break;
                default:
                    monster.hp = 0;
                    monster.atk = 0;
                    break;

            }
        }

        static void Fight(ref Player player, ref Monster monster)
        {
            while(true)
            {
                // 플레이어 먼저 몬스터 공격
                monster.hp -= player.atk;
                if(monster.hp <= 0)
                {
                    Console.WriteLine("승리!! 몬스터를 처치했습니다.");
                    Console.WriteLine($"남은 체력 {player.hp}");
                    break;
                }

                // 몬스터 반격
                player.hp -= monster.atk;
                if(player.hp <= 0)
                {
                    Console.WriteLine("패배!!");
                    break;
                }

            }

        }

        static void EnterField(ref Player player)
        {
            while(true)
            {
                Console.WriteLine("필드에 들어왔습니다.");

                // 랜덤으로 몬스터 리스폰
                Monster monster;
                CreateRandomMonster(out monster);  // CreateRandomMonster함수 실행.

                Console.WriteLine(" [1] 전투모드로 돌입.");
                Console.WriteLine(" [2] 도망쳐나간다.");

                string input = Console.ReadLine( );

                if(input == "1")
                {
                    Fight(ref player, ref monster);
                }
                else if(input == "2")
                {
                    Random random = new Random( );
                    int randomValue = random.Next(1, 101);

                    if(randomValue <= 33)
                    {
                        Console.WriteLine("도망치는데 성공했습니다.");
                        break;
                    }
                    else
                    {
                        Fight(ref player, ref monster);
                    }
                }
                else
                    break;
            }
        }

        static void EnterGame(ref Player player)
        {
            while(true)
            {
                Console.WriteLine("게임에 접속했습니다.");
                Console.WriteLine(" [1] 필드로 간다.");
                Console.WriteLine(" [2] 로비로 돌아간다.");

                string input = Console.ReadLine( );
                if(input == "1")
                {
                    EnterField(ref player);
                }
                else if(input == "2")
                {

                }
            }
        }

        static void Main( string[] args )
        {
            while(true)
            {
                ClassType choice = ChooseClass( );
                // ClassType 형식의 변수choice는 ChooseClass()함수를 호출.
                
                // 조건문 ClassTpye이 None이 아닐 때(1or2or3을 선택했을 때)
                if(choice != ClassType.None)    
                {
                    // 캐릭터 생성
                    Player player;  // struct구조체 타입의 player선언.                    
                    CreatePlayer(choice, out player);   // CreatePlayer() 함수 실행.

                    Console.WriteLine($"{choice} HP : {player.hp} ATK : {player.atk}");

                    EnterGame(ref player );

                }

            }
        }
    }
}