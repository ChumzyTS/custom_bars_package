# Custom Bars Unity Package
A custom Unity package I made that lets you easily create UI bars.

How to use:

background = The Background of the bar
bar = The actual bar that will have its size changed

anchor = which side will be anchored to the background

textDisplay = True if you want to display text as part of the bar

Text Styles
The way text stles work is pretty simple

- [Cur] will be converted to the current value
- [Max] will be converted to the max values
- [Perc] will be converted to the percentage (Cur/Max) floored to the amount of demical points specified

NOTE the default style "[Cur][Max]" is actually "[Cur]/[Max]"
