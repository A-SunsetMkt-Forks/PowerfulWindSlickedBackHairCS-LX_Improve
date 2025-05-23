﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace PowerfulWindSlickedBackHair.Windows
{
	// Token: 0x0200000D RID: 13
	public partial class DogWalkF : Form
	{
        private Bitmap walk1;

        private Bitmap walk2;

        public new Image BackgroundImage
        {
            get
            {
                if (this.IsDisposed) return null;
                if (InvokeRequired)
                {
                    return (Image)Invoke(new Func<Image>(() => base.BackgroundImage));
                }
                return base.BackgroundImage;
            }
            set
            {
                if (IsDisposed)
                    return;
                if (InvokeRequired)
                {
                    BeginInvoke((MethodInvoker)delegate
                    {
                        if (IsDisposed || !IsHandleCreated)
                            return;
                        Image oldImage = base.BackgroundImage;
                        base.BackgroundImage = value;
                    });
                }
                else
                {
                    if (IsDisposed || !IsHandleCreated)
                        return;
                    Image oldImage = base.BackgroundImage;
                    base.BackgroundImage = value;
                }
            }
        }

        public DogWalkF()
        {
            InitializeComponent();
            base.StartPosition = FormStartPosition.Manual;
            walk1 = new Bitmap("Assets\\DogWalk1.png");
            walk2 = new Bitmap("Assets\\DogWalk2.png");
            //BackgroundImage = walk1;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, value: true);
            UpdateStyles();
            base.TopMost = true;
        }

        public void ShowDialog(Point pos, int endF)
        {
            base.TopMost = true;
            base.Location = pos;
            Thread backSwitcher = new Thread((ThreadStart)delegate
            {
                while (true)
                {
                    long num2 = Tracker.frame % 6;
                    BackgroundImage = ((num2 < 3) ? walk1 : walk2);
                    Thread.Sleep(8);
                }
            });
            Thread thread = new Thread((ThreadStart)delegate
            {
                int num = endF;
                backSwitcher.Start();
                do
                {
                    Thread.Sleep(8);
                }
                while (Tracker.frame <= endF);
                backSwitcher.Abort();
                Hide();
            });
            thread.Start();
            ShowDialog();
        }
    }
}
