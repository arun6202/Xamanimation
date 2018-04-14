namespace Xamanimation
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Xamarin.Forms;

	[ContentProperty("Animations")]
	public class StoryBoard : AnimationBase
	{
		public StoryBoard()
		{
			Animations = new List<AnimationBase>();
		}

		public StoryBoard(List<AnimationBase> animations)
		{
			Animations = animations;
		}

		public List<AnimationBase> Animations
		{
			get;
		}

		protected override async Task BeginAnimation()
		{
			if (Combine)
			{
				var combinedTasks = new List<Task>();

				foreach (var animation in Animations)
				{
					if (animation.Target == null)
						animation.Target = Target;

					combinedTasks.Add(animation.Begin());

					RunUITask(combinedTasks);
				}
			}
			else
			{
				foreach (var animation in Animations)
				{
					if (animation.Target == null)
						animation.Target = Target;

					await animation.Begin();
				}
			}

		}
		protected override async Task ResetAnimation()
		{
			if (Combine)
			{
				var combinedTasks = new List<Task>();

				foreach (var animation in Animations)
				{
					if (animation.Target == null)
						animation.Target = Target;

					combinedTasks.Add(animation.Reset());

					RunUITask(combinedTasks);
				}
			}
			else
			{
				foreach (var animation in Animations)
				{
					if (animation.Target == null)
						animation.Target = Target;

					await animation.Reset();
				}
			}

		}

		protected override async Task RepeatAnimation()
        {
            if (Combine)
            {
                var combinedTasks = new List<Task>();

                foreach (var animation in Animations)
                {
                    if (animation.Target == null)
                        animation.Target = Target;

					combinedTasks.Add(animation.Repeat());

                    RunUITask(combinedTasks);
                }
            }
            else
            {
                foreach (var animation in Animations)
                {
                    if (animation.Target == null)
                        animation.Target = Target;

					await animation.Repeat();
                }
            }

        }
		private void RunUITask(List<Task> combinedTasks)
		{
			Device.BeginInvokeOnMainThread(() => Task.WhenAll(combinedTasks));

		}
	}
}
