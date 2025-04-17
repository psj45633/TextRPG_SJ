
using static System.Formats.Asn1.AsnWriter;
using System.Numerics;

namespace TextRPG_SJ__
{
    internal class Program
    {
        // 1. 게임시작 화면
        // 2. 상태 보기
        // 3. 인벤토리
        // 4. 장착관리
        // 5. 상점
        // 6. 아이템구매
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
        public int atk = 10;
        public int def = 5;
        public int hp = 100;
        public int gold = 50000;

        ItemManager itemManager;
        Item item;

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
                Console.Write("1. 상태 보기\n2. 인벤토리\n3. 상점\n\n원하시는 행동을 입력해주세요.\n>>");

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
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }

        }

        private void PlayerStat()
        {
            Console.WriteLine("상태 보기\n캐릭터의 정보가 표시됩니다.\n");
            Console.WriteLine($"Lv. {lv.ToString("D2")}\nChad ( {job} )\n공격력 : {atk}\n방어력 : {def}\n체력 : {hp}\nGold : {gold} G\n");
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
        Item item;

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
        }

        private void Equip()
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
                Console.WriteLine($"- {item.ItemName} | {item.StatName} +{item.Stat} | {item.Info} | {(item.IsBuy ? "구매완료" : $"{item.BuyGold} G")}");
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


        }

        public void IntoBuy()
        {


            Console.WriteLine($"상점 - 아이템 구매\n필요한 아이템을 얻을 수 있는 상점입니다.\n\n[보유 골드] G\n{gameLogic.gold}\n\n[아이템 목록]");
            int idx = 1;
            foreach (var item in Item.items)
            {
                Console.WriteLine($"- {idx} {item.ItemName} | {item.StatName} +{item.Stat} | {item.Info} | {(item.IsBuy ? "구매완료" : $"{item.BuyGold} G")}"); idx++;
            }
            Console.Write("\n0. 나가기\n\n원하시는 행동을 입력해주세요.\n>>");
            int select = int.Parse(Console.ReadLine());
            switch (select)
            {
                case 0:
                    IntoStore(); break;
                case 1: BuyItem(1); break;
                case 2: BuyItem(2); break;
                case 3: BuyItem(3); break;
                case 4: BuyItem(4); break;
                case 5: BuyItem(5); break;
                case 6: BuyItem(6); break;



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
            //if (itemindex > 0 && itemindex <= 6)
            //{
            //    myItem.Add(Item.items[itemindex - 1]);
            //    item.IsBuy = true;
            //    gameLogic.gold -= Item.items[itemindex - 1].BuyGold;

            //    Console.WriteLine($"{myItem[itemindex - 1].ItemName}구매가 완료되었습니다.");
            //}
            //IntoBuy();
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
            items.Add(new Item(false, "낡은 검", "공격력", 2, "쉽게 볼 수 있는 낡은 검입니다.", 600, false));
            items.Add(new Item(false, "청동 도끼", "공격력", 5, "어디선가 사용됐던거 같은 입니다.", 1500, false));
            items.Add(new Item(false, "스파르타의 창", "공격력", 7, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 2500, false));
        }
    }
}
