   M S B A M L   `   `   `  ����K  GAirportSimulator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:AirportSimulatorAirportSimulator  5   	   %    AirportSimulator.AirplaneWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:AirportSimulator     5   	     � SizeToContent$  WidthAndHeight=�5   	    � 
ResizeMode$ NoResize=�5	   	    � Title$ AirplaneWindow��5
   	   .��5      � $��15q�6    � ColumnDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   .+�5   
   �� $��0��6   $��0��6   $��Flight number:��6-   �� 5   
   $��0��6   $��1��6!   ��61   �� *
FlightID+�� 5   
   $��1��6   $��0��6   $��Destination:��6-   �� 5   
   -   5        $��txtDest��5   N   $��1��6   $��1��6!   ��61   �� *Destination+6N   �� 5   
   $��2��6   $��0��6   $��Flight time:��6-   �� 5   
   $��2��6   $��1��6!   ��61   �� *
FlightTime+�� 5   
   $��3��6   $��0��6   $��	Altitude:��6-   �� 5   
   -   5        $��txtAlt��5   K   $��3��6   $��1��6!   ��61   �� *
Altitude+6K   �� 5    
   $��4��6   $��0��6   $��Status:��6-   �� 5!   
   -   5        $��	txtStatus��5!   I   $��4��6   $��1��6!   ��61   �� *Status+6I   �� 5"   
    �� Orientation$ 
Horizontal=�6   $��Center=�6.   $��Center=�6K   $��5��6g   $��0��6t   $��2��6�   .+�5#      �� -   5        $	��100��5#      $��Center=�6!   $��Center=�6>   $��Take off��6Y   $��5q�6�   �� 5$      -   5        $	��100��5$      $��Center=�6!   $��Center=�6>   $
��Land��6Y   6h   5%      5'      5(      