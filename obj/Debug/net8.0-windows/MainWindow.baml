   M S B A M L   `   `   `  ����K  GAirportSimulator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:AirportSimulatorAirportSimulator  5   	   !    AirportSimulator.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:AirportSimulator     5   	     � Title$)  #Airport Simulator By Ronja Sjögren��5   	    � SizeToContent$ WidthAndHeight=�5	   	    � 
ResizeMode$ NoResize=�5
   	   .��5      � $��15q�6    � ColumnDefinitions 5   
   �� 5      $	��275 �6   �� 5      $
��Auto �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   � $��0��6   $��0��6   $��5q�6,    5      �� 5      $��* �6#   �� 5      $��* �6#    5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    .+�5!      �� $��0��6   $��0��6!   $
��Name��61   �� 5"      -   5        $��txtName��5"   3   $��0��6   $��1��6#   $��3q�6B   $��
0, 3, 0, 0q�6N   �� 5$      $��1��6   $��0��6!   $��	Flight ID��61   �� 5%      -   5        $��txtFlightID��5%   3   $��1��6   $��1��6#   $��3q�6F   $��
0, 3, 0, 0q�6R   �� 5'      $��2��6   $��0��6!   $��Destination��61   �� 5(      -   5        $��txtDestination��5(   3   $��2��6   $��1��6#   $��3q�6I   $��
0, 3, 0, 0q�6U   �� 5*      $��3��6   $��0��6!   $��Flight time��61   �� 5+      -   5        $��txtFlightTime��5+   3   $��3��6   $��1��6#   $��3q�6H   $��
0, 3, 0, 0q�6T   5,      �� 5-   
   $
��Auto=�6   $
��Auto=�6:   $��1��6_   $��0��6o   $	��450��6|   $��10, 0, 0, 0q�6�   .��5.      �� -   5        $��	lstPlanes��5.      $	��130��6,    �� View 5/      �� 50      .P�51      �� $
��Name��6-    �� Width$	 120��6a    �� DisplayMemberBinding 6;   �� *Name+6a   �� 52      $��	Flight ID��6-   $ 85��6j    6@   �� *
FlightID+6j   �� 53      $��Destination��6-   $	 150��6o    6B   �� *Destination+6o   �� 54      $��Flight time��6-   $ 75��6n    6B   �� *
FlightTime+6n   55      57      58      �� 5;   
   $��1��6   $��0��6"   $��2��62    �� Orientation$ 
Horizontal=�6F   $��Center=�6_   $��Center=�6z   $��15q�6�   .+�5<      �� -   5        $��btnAdd��5<      $��	Add plane��6#   $	��150��6L   $��5q�6X   �� 5=      -   5        $��
btnTakeOff��5=      $��Take off��6'   $	��150��6S   $��5q�6_   $��15, 0q�6k   �� 5>      -   5        $��	btnCngAlt��5>      $��Request altitude change��6&   $	��150��6h   $��5q�6t   5?      �� 5A   
   $��0��6   $��2��6'   $��2��6;   $
��Auto=�6H   $
��Auto=�6k   .��5B      �� -	   5        $��lstMessages��5B   $   $	��250��6   6$   5C      5G      5H      