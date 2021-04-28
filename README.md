# RendererHelpers
A set of methods that reduce the boiler plate code of custom renderers or effects in Xamarin.Forms.
And it really decreases the amount of code required to produce a well standing custom renderer.

* HandlePropertyChanges

Typically, In the process of creating a XamarinForms custom renderer\effect, you have to make sure that the control is responsive to any property change and that it must reflect to the screen if it affects the appearance of the control.
Following [Xamarin.Forms Samples](https://developer.xamarin.com/samples/xamarin-forms/all/) and a lot of XF MVPs, you override the `OnElementPropertyChanged` method which by default provides a `PropertyChangedEventArgs` argument. The `PropertyChangedEventArgs` contains a reference to the name of the property has been changed. And through a lot of if\else statements you really can trace the changes and reflect them. 

### Example using the traditional if\else approach:

```
 protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
 {
    base.OnElementPropertyChanged(args);
    if (args.PropertyName == Effects.DropShadow.BlurRadiusProperty.PropertyName)
    {
        UpdateBlurRadius();
        UpdateShadow();
    }
    else if (args.PropertyName == Effects.DropShadow.ShadowOpacityProperty.PropertyName)
    {
        UpdateShadowOpacity();
        UpdateShadow();
    }
    else if (args.PropertyName == Effects.DropShadow.ShadowOpacityProperty.PropertyName)
    {
        UpdateShadowOpacity();
        UpdateShadowOpacity();
    }
    else if (args.PropertyName == Effects.DropShadow.ShadowColorProperty.PropertyName)
    {
        UpdateShadowColor();
        UpdateShadowOpacity();
    }
    else if (args.PropertyName == Effects.DropShadow.OffsetXProperty.PropertyName)
    {
        UpdateOffset();
        UpdateShadowOpacity();
    }
    else if (args.PropertyName == Effects.DropShadow.OffsetYProperty.PropertyName)
    {
        UpdateOffset();
        UpdateShadowOpacity();
    } 
 }
 ```
 
Using `RendererHelpers.HandlePropertyChanges` method you can achieve every thing you achieved using the if\else approach except using fewer lines of code. And more importantly, much more convenient.
 
 ### Example using the `RendererHelpers` approach:
 
 ```
 protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
  {
  
    base.OnElementPropertyChanged(args);
    
    RendererHelpers.Helpers.HandlePropertyChanges<Controls.DropShadowPanel>(args, new    System.Collections.Generic.Dictionary<BindableProperty, System.Action>()
    {
    
      {Effects.DropShadow.OffsetXProperty, UpdateOffset },
      {Effects.DropShadow.OffsetYProperty, UpdateOffset },
      {Effects.DropShadow.BlurRadiusProperty, UpdateBlurRadius},
      {Effects.DropShadow.ShadowOpacityProperty, UpdateShadowOpacity},
      {Effects.DropShadow.ShadowColorProperty, UpdateShadowColor},
      
    }, UpdateShadow); 
    
   }
```
    
    
### Contributions 
All contributions and issues are welcomed and will be discussed in order to improve RendererHelpers
