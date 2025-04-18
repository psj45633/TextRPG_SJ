
using static System.Formats.Asn1.AsnWriter;
using System.Numerics;
using System.ComponentModel;

namespace TextRPG_SJ__
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameLogic gameLogic = new GameLogic();
            gameLogic.StartGame();
        }
    }

    class GameLogic
    {
        bool isPlaying = true;
        public int lv = 1;
        string job = "전사";
        int baseAtk = 10;
        int baseDef = 5;
        public int atk = 10;
        public int def = 5;
        public int hp = 100;
        public int gold = 50000;

        ItemManager itemManager;


        public GameLogic()
        {
            itemManager = new ItemManager(this);
        }

        public void StartGame()
        {
            Item.AddListItem();
            Console.WriteLine("오류 21개 났조에 오신 여려분들 환영합니다.\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
            Init();
        }

        public void Init()
        {
            while (isPlaying)
            {
                Console.Write("1. 상태 보기\n2. 인벤토리\n3. 상점\n4. 던전 입장\n5. 휴식하기\n\n원하시는 행동을 입력해주세요.\n>>");

                int select = int.Parse(Console.ReadLine());
                if (select == 1) // 1. 상태 보기
                {
                    PlayerStat();
                }
                else if (select == 2) // 2. 인벤토리
                {
                    itemManager.MyItemInfo();
                }
                else if (select == 3) // 3. 상점
                {
                    itemManager.IntoStore();
                }
                else if (select == 4)
                {
                    IntoDungeon();
                }

                else if (select == 5)
                {
                    Rest();
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }

        }

        private void IntoDungeon()
        {
            bool isClear = false;
            int easyDef = 5;
            int normalDef = 11;
            int hardDef = 17;
            int easyReward = 1000;
            int normalReward = 1700;
            int hardReward = 2500;
            

            Random random = new Random();
            int successRate = random.Next(1, 101);
            int damage = random.Next(20,35);
            int rewardRate = random.Next(1, 3);

            int afterHp;
            int afterGold;
            

            Console.WriteLine($"던전입장\n이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n( 공격력 : {atk} | 방어력 : {def} )\n");
            Console.WriteLine($"1. Easy \t| 방어력 {easyDef} 이상 권장\n2. Normal\t| 방어력 {normalDef} 이상 권장\n3. Hard \t| 방어력 {hardDef} 이상 권장\n0. 나가기\n");
            int select = int.Parse(Console.ReadLine());
            if (select == 1) // Easy 입장
            {
                Console.WriteLine("입장 하시겠습니까?\n1. 예\n2. 아니오");
                int select2 = int.Parse(Console.ReadLine());
                if (select2 == 1)
                {
                    if (def > easyDef || def <= easyDef && successRate > 40)
                    {
                        afterHp = hp - damage - (easyDef - def);
                        afterGold = gold + easyReward * (1 + (atk * rewardRate) / 100);
                        if (afterHp > 100) afterHp = 100;
                        Console.WriteLine($"던전 클리어\n축하합니다!!\nEasy를 클리어 하였습니다.\n\n[탐험 결과]\n체력 {hp} -> {afterHp}");
                        Console.WriteLine($"Gold {gold} -> {afterGold} G\n\n0. 나가기\n");
                        hp = afterHp;
                        gold = afterGold;
                        

                        while (true)
                        {
                            Console.Write("원하시는 행동을 입력해주세요.\n>> ");
                            int select3 = int.Parse(Console.ReadLine());
                            if (select3 == 0)
                            {
                                IntoDungeon();
                            }
                            else
                            {
                                Console.WriteLine("잘못된 입력입니다.");
                            }
                        }

                    }
                    else
                    {
                        afterHp = hp / 2;
                        Console.WriteLine($"실패\n던전 도전에 실패하였습니다...\n\n[탐험 결과]\n체력 {hp} -> {afterHp}\n\n0. 나가기\n");

                        hp = afterHp;
                        while (true)
                        {
                            Console.Write("원하시는 행동을 입력해주세요.\n>> ");
                            int select3 = int.Parse(Console.ReadLine());
                            if (select3 == 0)
                            {
                                IntoDungeon();
                            }
                            else
                            {
                                Console.WriteLine("잘못된 입력입니다.");
                            }
                        }

                    }
                }
                else
                {
                    IntoDungeon();
                }
            } // Easy 입장
            else if (select == 2) // Normal 입장
            {
                Console.WriteLine("입장 하시겠습니까?\n1. 예\n2. 아니오");
                int select2 = int.Parse(Console.ReadLine());
                if (select2 == 1)
                {
                    if (def > normalDef || def <= normalDef && successRate > 40)
                    {
                        afterHp = hp - damage - (normalDef - def);
                        afterGold = gold + normalReward * (1 + (atk * rewardRate) / 100);
                        if (afterHp > 100) afterHp = 100;
                        Console.WriteLine($"던전 클리어\n축하합니다!!\nNormal을 클리어 하였습니다.\n\n[탐험 결과]\n체력 {hp} -> {afterHp}");
                        Console.WriteLine($"Gold {gold} -> {afterGold} G\n\n0. 나가기\n");
                        hp = afterHp;
                        gold = afterGold;
                        

                        while (true)
                        {
                            Console.Write("원하시는 행동을 입력해주세요.\n>> ");
                            int select3 = int.Parse(Console.ReadLine());
                            if (select3 == 0)
                            {
                                IntoDungeon();
                            }
                            else
                            {
                                Console.WriteLine("잘못된 입력입니다.");
                            }
                        }
                    }
                    else
                    {
                        afterHp = hp / 2;
                        Console.WriteLine($"실패\n던전 도전에 실패하였습니다...\n\n[탐험 결과]\n체력 {hp} -> {afterHp}\n\n0. 나가기\n");

                        hp = afterHp;
                        while (true)
                        {
                            Console.Write("원하시는 행동을 입력해주세요.\n>> ");
                            int select3 = int.Parse(Console.ReadLine());
                            if (select3 == 0)
                            {
                                IntoDungeon();
                            }
                            else
                            {
                                Console.WriteLine("잘못된 입력입니다.");
                            }
                        }
                    }
                }
                else
                {
                    IntoDungeon();
                }
            } // Normal 입장
            else if (select == 3) // Hard 입장
            {
                Console.WriteLine("입장 하시겠습니까?\n1. 예\n2. 아니오");
                int select2 = int.Parse(Console.ReadLine());
                if (select2 == 1)
                {
                    if (def > hardDef || def <= hardDef && successRate > 40)
                    {
                        afterHp = hp - damage - (hardDef - def);
                        afterGold = gold + hardReward * (1 + (atk * rewardRate) / 100);
                        if (afterHp > 100) afterHp = 100;
                        Console.WriteLine($"던전 클리어\n축하합니다!!\nHard를 클리어 하였습니다.\n\n[탐험 결과]\n체력 {hp} -> {afterHp}");
                        Console.WriteLine($"Gold {gold} -> {afterGold} G\n\n0. 나가기\n");
                        hp = afterHp;
                        gold = afterGold;
                        
                        

                        while (true)
                        {
                            Console.Write("원하시는 행동을 입력해주세요.\n>> ");
                            int select3 = int.Parse(Console.ReadLine());
                            if (select3 == 0)
                            {
                                IntoDungeon();
                            }
                            else
                            {
                                Console.WriteLine("잘못된 입력입니다.");
                            }
                        }

                    }
                    else
                    {
                        afterHp = hp / 2;
                        Console.WriteLine($"실패\n던전 도전에 실패하였습니다...\n\n[탐험 결과]\n체력 {hp} -> {afterHp}\n\n0. 나가기\n");

                        hp = afterHp;
                        while (true)
                        {
                            Console.Write("원하시는 행동을 입력해주세요.\n>> ");
                            int select3 = int.Parse(Console.ReadLine());
                            if (select3 == 0)
                            {
                                IntoDungeon();
                            }
                            else
                            {
                                Console.WriteLine("잘못된 입력입니다.");
                            }
                        }

                    }
                }
                else
                {
                    IntoDungeon();
                }
            } // Hard 입장
            else if (select ==0)
            {
                Init();
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                IntoDungeon();
            }

        }

        private void Rest()
        {

            Console.WriteLine($"휴식하기\n500 G를 내면 체력을 회복할 수 있습니다. (보유 골드 : {gold} G\n\n1. 휴식하기\n0. 나가기\n");
            Console.Write("원하시는 행동을 입력해주세요\n>>");
            int select = int.Parse(Console.ReadLine());

            if (select == 1 && gold >= 500)
            {
                gold -= 500;
                hp = 100;
                Console.WriteLine("체력이 회복 되었습니다.");
            }
            else if (select == 1 && gold < 500)
            {
                Console.WriteLine("돈이 부족합니다.");
                Rest();
            }
            else if (select == 0)
            {
                Init();
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                Rest();
            }
        }

        private void PlayerStat()
        {
            Console.WriteLine("상태 보기\n캐릭터의 정보가 표시됩니다.\n");
            Console.WriteLine($"Lv. {lv.ToString("D2")}\nChad ( {job} )\n공격력 : {atk} (+{atk - baseAtk})\n방어력 : {def} (+{def - baseDef})\n체력 : {hp}\nGold : {gold} G\n");
            Console.Write("0. 나가기\n\n원하시는 행동을 입력해주세요.\n>>");
            int select = int.Parse(Console.ReadLine());
            if (select == 0)
            {
                Init();
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                PlayerStat();
            }
        }
    }

    class ItemManager
    {
        GameLogic gameLogic;
        public ItemManager(GameLogic logic)
        {
            gameLogic = logic;
        }

        public List<Item> myItem = new List<Item>();

        public void MyItemInfo()
        {
            Console.WriteLine("인벤토리\n보유 중인 아이템을 관리할 수 있습니다.\n\n[아이템 목록]");

            if (myItem.Count > 0)
            {
                foreach (Item item in myItem)
                {
                    Console.WriteLine($"- {(item.IsEquip ? "[E]" : "")}{item.ItemName} | {item.StatName} +{item.Stat} | {item.Info}");
                }
            }
            else
            {
                Console.WriteLine("보유한 아이템이 없습니다.");
            }

            Console.Write("1. 장착 관리\n2. 나가기\n\n원하시는 행동을 입력해주세요.\n>>");

            int select = int.Parse(Console.ReadLine());
            if (select == 1) // 장착관리
            {
                Equip();
            }
            else if (select == 2)
            {

            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                MyItemInfo();
            }
        }

        public void Equip()
        {
            Console.WriteLine("인벤토리 - 장착관리\n보유중인 아이템을 관리할 수 있습니다.\n\n[아이템 목록]");
            int idx = 1;
            foreach (Item item in myItem)
            {
                Console.WriteLine($"- {idx} {(item.IsEquip ? "[E]" : "")}{item.ItemName} | {item.StatName} +{item.Stat} | {item.Info} "); idx++;
            }
            Console.Write("\n0. 나가기\n\n원하시는 행동을 입력해주세요.\n>>");
            int select = int.Parse(Console.ReadLine());
            if (select == 0)
            {
                return;
            }
            else if (select > 0 && select <= myItem.Count)
            {
                Item selectedItem = myItem[select - 1];  //  선택한 아이템
                if (!selectedItem.IsEquip) // 템 장착
                {
                    selectedItem.IsEquip = true;

                    if (selectedItem.StatName == "공격력")
                        gameLogic.atk += selectedItem.Stat;

                    else if (selectedItem.StatName == "방어력")
                        gameLogic.def += selectedItem.Stat;

                    Console.WriteLine($"{selectedItem.ItemName}을(를) 장착했습니다.");
                }
                else // 템 해제
                {
                    selectedItem.IsEquip = false;

                    if (selectedItem.StatName == "공격력")
                        gameLogic.atk -= selectedItem.Stat;

                    else if (selectedItem.StatName == "방어력")
                        gameLogic.def -= selectedItem.Stat;

                    Console.WriteLine($"{selectedItem.ItemName}을(를) 해제했습니다.");
                }
                Equip();
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }

        public void IntoStore()
        {

            Console.WriteLine($"상점\n필요한 아이템을 얻을 수 있는 상점입니다.\n\n[보유 골드] G\n{gameLogic.gold}\n\n[아이템 목록]");
            foreach (var item in Item.items) // 예외발생 - List<Item>을 static으로 해줘서 해결
            {
                Console.WriteLine($"- {item.ItemName} | {item.StatName} +{item.Stat} | {item.Info} | {(item.IsBuy ? "[구매완료]" : $"{item.BuyGold} G")}");
            }
            Console.WriteLine("\n1. 아이템 구매\n0. 나가기");
            int select = int.Parse(Console.ReadLine());
            if (select == 0)
            {
                gameLogic.Init();
            }
            else if (select == 1)
            {
                IntoBuy();
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                IntoStore();
            }
        }

        public void IntoBuy()
        {
            Console.WriteLine($"상점 - 아이템 구매\n필요한 아이템을 얻을 수 있는 상점입니다.\n\n[보유 골드] G\n{gameLogic.gold}\n\n[아이템 목록]");
            int idx = 1;
            foreach (var item in Item.items)
            {
                Console.WriteLine($"- {idx} {item.ItemName} | {item.StatName} +{item.Stat} | {item.Info} | {(item.IsBuy ? "[구매완료]" : $"{item.BuyGold} G")}"); idx++;
            }
            Console.Write("\n0. 나가기\n\n원하시는 행동을 입력해주세요.\n>>");
            int select = int.Parse(Console.ReadLine());
            switch (select)
            {
                case 0: IntoStore(); break;
                case 1: BuyItem(1); break;
                case 2: BuyItem(2); break;
                case 3: BuyItem(3); break;
                case 4: BuyItem(4); break;
                case 5: BuyItem(5); break;
                case 6: BuyItem(6); break;
                case 7: BuyItem(7); break;
                case 8: BuyItem(8); break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    IntoBuy();
                    break;
            }
        }

        public void BuyItem(int itemindex)
        {
            if (itemindex > 0 && itemindex <= Item.items.Count)
            {
                Item selectedItem = Item.items[itemindex - 1];

                if (selectedItem.IsBuy)
                {
                    Console.WriteLine("이미 구매한 아이템입니다.");
                }
                else if (gameLogic.gold < selectedItem.BuyGold)
                {
                    Console.WriteLine("골드가 부족합니다.");
                }
                else
                {
                    myItem.Add(selectedItem);
                    selectedItem.IsBuy = true;
                    gameLogic.gold -= selectedItem.BuyGold;

                    Console.WriteLine($"{selectedItem.ItemName} 구매가 완료되었습니다.");
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
            IntoBuy();
        }
    }

    class Item
    {
        public string ItemName;
        public string StatName;
        public int Stat;
        public string Info;
        public int BuyGold;
        public bool IsBuy = false;
        public bool IsEquip = false;

        public Item(bool isEquip, string itemName, string statName, int stat, string info, int buygold, bool isBuy)
        {
            ItemName = itemName;
            StatName = statName;
            Stat = stat;
            Info = info;
            BuyGold = buygold;
            IsBuy = isBuy;
            IsEquip = isEquip;
        }

        public static List<Item> items = new List<Item>();
        public static void AddListItem()
        {
            items.Add(new Item(false, "수련자 갑옷", "방어력", 5, "수련에 도움을 주는 갑옷입니다.", 1000, false));
            items.Add(new Item(false, "무쇠갑옷", "방어력", 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 2000, false));
            items.Add(new Item(false, "스파르타의 갑옷", "방어력", 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500, false));
            items.Add(new Item(false, "나영웅의 갑옷", "방어력", 35, "스파르타의 전사 나영웅이 사용했다는 전설의 갑옷입니다.", 15000, false));
            items.Add(new Item(false, "낡은 검", "공격력", 2, "쉽게 볼 수 있는 낡은 검입니다.", 600, false));
            items.Add(new Item(false, "청동 도끼", "공격력", 5, "어디선가 사용됐던거 같은 입니다.", 1500, false));
            items.Add(new Item(false, "스파르타의 창", "공격력", 7, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 2500, false));
            items.Add(new Item(false, "박찬우의 창", "공격력", 20, "스파르타의 전사 박찬우가 사용했다는 전설의 창입니다.", 15000, false));
        }
    }
}
