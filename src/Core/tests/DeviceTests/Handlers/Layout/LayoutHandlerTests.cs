using Microsoft.Maui.Handlers;
using System.Threading.Tasks;
using Microsoft.Maui.DeviceTests.Stubs;
using Xunit;

namespace Microsoft.Maui.DeviceTests.Handlers.Layout
{
	public partial class LayoutHandlerTests : HandlerTestBase<LayoutHandler>
	{
		[Fact(DisplayName = "[LayoutHandler] Empty layout")]
		public async Task EmptyLayout()
		{
			var layout = new LayoutStub();
			await ValidatePropertyInitValue(layout, () => layout.Children.Count, GetNativeChildCount, 0);
		}

		[Fact(DisplayName = "[LayoutHandler] Handler view count matches layout view count")]
		public async Task HandlerViewCountMatchesLayoutViewCount()
		{
			var layout = new LayoutStub();

			layout.Add(new SliderStub());
			layout.Add(new SliderStub());

			await ValidatePropertyInitValue(layout, () => layout.Children.Count, GetNativeChildCount, 2);
		}

		[Fact(DisplayName = "[LayoutHandler] Handler removes child from native layout")]
		public async Task HandlerRemovesChildFromNativeLayout()
		{
			var layout = new LayoutStub();
			var slider = new SliderStub();
			layout.Add(slider);

			var handler = await CreateHandlerAsync(layout);

			var count = await InvokeOnMainThreadAsync(() =>
			{
				handler.Remove(slider);
				return GetNativeChildCount(handler);
			});

			Assert.Equal(0, count);
		}
	}
}
