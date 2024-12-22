using FinalProject.Enemies;
using FinalProject.Heroes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FinalProject
{
    public partial class Form2 : Form
    {
        public static Form2 instance;

        public Random random = new Random();
        private int moveStep = 67;

        // 我方角色创建
        public Hero player = new MonkeyKing(100, 10, 5, 70, 150, 5, 5, 100);

        public List<Hero> heroes = new List<Hero>
        {
            new Pigsy(100, 10, 5, 20, 150, 5, 5, 100),
            new Sandy(100, 10, 5, 20, 150, 5, 5, 100),
        };

        // 敌方角色创建
        public List<Enemy> enemies = new List<Enemy>
        {
            new ElderJinchi(100, 10, 5, 20, 150, 5, 5, LootType.Experience, 2),
            new TigerGeneral(100, 10, 5, 20, 150, 5, 5, LootType.Coin, 3),
            new ErlangShen(100, 10, 5, 20, 150, 5, 5, LootType.Experience, 5),
        };

        public List<Enemy> enemiesToAD = new List<Enemy>();
        public List<Enemy> enemiesToAP = new List<Enemy>();

        public Form2()
        {
            InitializeComponent();
            instance = this;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            PrintAttributes();

            // 操作提示字体为黑色
            richTextBox2.SelectionColor = Color.Black;
            richTextBox2.AppendText($"方向键移动" + Environment.NewLine);
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            richTextBox2.Clear();

            #region PlayerAction
            // 阻止按键音
            e.SuppressKeyPress = true;

            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (player.yPosition > 0)
                    {
                        player.yPosition -= 1;
                        pictureBox1.Top -= moveStep;
                    }
                    break;
                case Keys.Down:
                    if (player.yPosition < 4)
                    {
                        player.yPosition += 1;
                        pictureBox1.Top += moveStep;
                    }
                    break;
                case Keys.Left:
                    if (player.xPosition > 0)
                    {
                        player.xPosition -= 1;
                        pictureBox1.Left -= moveStep;
                    }
                    break;
                case Keys.Right:
                    if (player.xPosition < 14)
                    {
                        player.xPosition += 1;
                        pictureBox1.Left += moveStep;
                    }
                    break;
                case Keys.D1:
                    if (enemiesToAD.Count > 0)
                    {
                        foreach (var enemy in enemiesToAD)
                        {
                            richTextBox2.SelectionColor = Color.Green;
                            player.DoAttack(enemy);
                        }
                    }
                    break;
                case Keys.D2:
                    if (enemiesToAP.Count > 0)
                    {
                        foreach (var enemy in enemiesToAP)
                            player.DoMagicalAttack(enemy, Color.Green);
                    }
                    break;
            }
            #endregion

            foreach (var partner in heroes)
                PartnersAction(partner);

            foreach (var enemy in enemies)
                EnemiesAction(enemy);

            // 删除死掉的角色
            UpdateData();

            #region GameOverCheck
            if (player.isDead)
            {
                Controls.Clear();

                Label resultLabel = new Label();
                resultLabel.Text = "你死了，游戏结束！";
                resultLabel.Font = new Font("Arial", 20, FontStyle.Bold);
                resultLabel.AutoSize = false; // 禁用自动调整大小
                resultLabel.TextAlign = ContentAlignment.MiddleCenter; // 文本居中
                resultLabel.Dock = DockStyle.Fill; // 填充父容器
                resultLabel.BackColor = Color.LightGray; // 设置背景颜色
                Controls.Add(resultLabel);
            }

            bool gameOver = enemies.All(enemy => enemy.isDead);
            if (gameOver)
            {
                Controls.Clear();

                Label resultLabel = new Label();
                resultLabel.Text = "所有敌人已死，游戏结束！";
                resultLabel.Font = new Font("Arial", 20, FontStyle.Bold);
                resultLabel.AutoSize = false; // 禁用自动调整大小
                resultLabel.TextAlign = ContentAlignment.MiddleCenter; // 文本居中
                resultLabel.Dock = DockStyle.Fill; // 填充父容器
                resultLabel.BackColor = Color.LightGray; // 设置背景颜色
                Controls.Add(resultLabel);
            }
            #endregion

            // 清空文本
            richTextBox1.Clear();
            PrintAttributes();

            // 行动完之后清空列表
            enemiesToAD.Clear();
            enemiesToAP.Clear();

            // 判定玩家能否攻击敌人
            foreach (var enemy in enemies)
            {
                if (player.ADRange(enemy)) enemiesToAD.Add(enemy);
                if (player.APRange(enemy)) enemiesToAP.Add(enemy);
            }

            // 操作提示字体为黑色
            richTextBox2.SelectionColor = Color.Black;

            if (enemiesToAD.Count == 0 && enemiesToAP.Count == 0)
                richTextBox2.AppendText($"方向键移动" + Environment.NewLine);
            else if (enemiesToAD.Count == 0 && enemiesToAP.Count > 0)
                richTextBox2.AppendText($"方向键移动 2.法术攻击" + Environment.NewLine);
            else if (enemiesToAD.Count > 0)
                richTextBox2.AppendText($"方向键移动 1.物理攻击 2.法术攻击" + Environment.NewLine);

            // 让光标移动到内容末尾
            richTextBox2.SelectionStart = richTextBox1.Text.Length;
            richTextBox2.ScrollToCaret();
        }

        private void UpdateData()
        {
            PictureBox currentPictureBox = new PictureBox();

            for (int i = heroes.Count - 1; i >= 0; i--)
            {
                var hero = heroes[i];
                if (hero.isDead)
                {
                    switch (hero.name)
                    {
                        case "猪八戒":
                            currentPictureBox = pictureBox2;
                            break;
                        case "沙悟净":
                            currentPictureBox = pictureBox3;
                            break;
                    }
                    enemies.RemoveAt(i);
                    currentPictureBox.Dispose();
                }
            }

            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                var enemy = enemies[i];
                if (enemy.isDead)
                {
                    switch (enemy.name)
                    {
                        case "金池长老":
                            currentPictureBox = pictureBox4;
                            break;
                        case "二郎神":
                            currentPictureBox = pictureBox5;
                            break;
                        case "虎先锋":
                            currentPictureBox = pictureBox6;
                            break;
                    }
                    enemies.RemoveAt(i);
                    currentPictureBox.Dispose();
                }
            }
        }

        private void EnemiesAction(Enemy _enemy)
        {
            List<Hero> heroesToAD = new List<Hero>();
            List<Hero> heroesToAP = new List<Hero>();

            if (_enemy.ADRange(player)) heroesToAD.Add(player);
            if (_enemy.APRange(player)) heroesToAP.Add(player);

            foreach (var hero in heroes)
            {
                if (_enemy.ADRange(hero)) heroesToAD.Add(hero);
                if (_enemy.APRange(hero)) heroesToAP.Add(hero);
            }

            switch (random.Next(2))
            {
                case 0:
                    if (heroesToAD.Count > 0)
                    {
                        foreach (var hero in heroesToAD)
                        {
                            richTextBox2.SelectionColor = Color.Red;
                            _enemy.DoAttack(hero);
                        }
                    }
                    else
                        AIMove(_enemy);
                    break;
                case 1:
                    if (heroesToAP.Count > 0)
                    {
                        foreach (var hero in heroesToAP)
                            _enemy.DoMagicalAttack(hero, Color.Red);
                    }
                    else
                        AIMove(_enemy);
                    break;
            }
        }

        private void PartnersAction(Hero _partner)
        {
            List<Enemy> enemiesToAD = new List<Enemy>();
            List<Enemy> enemiesToAP = new List<Enemy>();

            foreach (var enemy in enemies)
            {
                if (_partner.ADRange(enemy)) enemiesToAD.Add(enemy);
                if (_partner.APRange(enemy)) enemiesToAP.Add(enemy);
            }

            switch (random.Next(2))
            {
                case 0:
                    if (enemiesToAD.Count > 0)
                    {
                        foreach (var enemy in enemiesToAD)
                        {
                            richTextBox2.SelectionColor = Color.Blue;
                            _partner.DoAttack(enemy);
                        }
                    }
                    else
                        AIMove(_partner);
                    break;
                case 1:
                    if (enemiesToAP.Count > 0)
                    {
                        foreach (var enemy in enemiesToAP)
                        {
                            _partner.DoMagicalAttack(enemy, Color.Blue);
                        }
                    }
                    else
                        AIMove(_partner);
                    break;
            }
        }

        private void AIMove(Entity _ai)
        {
            PictureBox currentPictureBox = new PictureBox();

            switch (_ai.name)
            {
                case "猪八戒":
                    currentPictureBox = pictureBox2;
                    break;
                case "沙悟净":
                    currentPictureBox = pictureBox3;
                    break;
                case "金池长老":
                    currentPictureBox = pictureBox4;
                    break;
                case "二郎神":
                    currentPictureBox = pictureBox5;
                    break;
                case "虎先锋":
                    currentPictureBox = pictureBox6;
                    break;
            }

            switch (random.Next(4))
            {
                case 0:
                    if (_ai.xPosition > 0)
                    {
                        _ai.xPosition -= 1;
                        currentPictureBox.Left -= moveStep;
                    }
                    else
                    {
                        _ai.xPosition += 1;
                        currentPictureBox.Left += moveStep;
                    }
                    break;
                case 1:
                    if (_ai.xPosition < 14)
                    {
                        _ai.xPosition += 1;
                        currentPictureBox.Left += moveStep;
                    }
                    else
                    {
                        _ai.xPosition -= 1;
                        currentPictureBox.Left -= moveStep;
                    }
                    break;
                case 2:
                    if (_ai.yPosition > 0)
                    {
                        _ai.yPosition -= 1;
                        currentPictureBox.Top -= moveStep;
                    }
                    else
                    {
                        _ai.yPosition += 1;
                        currentPictureBox.Top += moveStep;
                    }
                    break;
                case 3:
                    if (_ai.yPosition < 4)
                    {
                        _ai.yPosition += 1;
                        currentPictureBox.Top += moveStep;
                    }
                    else
                    {
                        _ai.yPosition -= 1;
                        currentPictureBox.Top -= moveStep;
                    }
                    break;
            }
        }

        private void PrintAttributes()
        {
            // 玩家绿色字体
            richTextBox1.SelectionColor = Color.Green;
            player.PrintAttributes(richTextBox1);

            // 友军黄色字体
            foreach (var hero in heroes)
            {
                richTextBox1.SelectionColor = Color.Blue;
                hero.PrintAttributes(richTextBox1);
            }

            // 敌方红色字体
            foreach (var enemy in enemies)
            {
                richTextBox1.SelectionColor = Color.Red;
                enemy.PrintAttributes(richTextBox1);
            }
        }

        // 开发时辅助用
        private void Form2_MouseDown(object sender, MouseEventArgs e)
        {
            // 获取鼠标当前的坐标
            int x = e.X;
            int y = e.Y;

            // 更新标签的文本以显示鼠标位置
            //this.Text = $"X: {x}, Y: {y}";
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
