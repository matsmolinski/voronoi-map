   M S B A M L   `   `   `  ����  LUPA    LUPA.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � 
ResizeMode$  NoResize=�5   	    � Title$
 LUPA��5   	   $	��700��5	   	   $	��914��5
   	    � WindowStartupLocation$ CenterScreen=�5   	    � Icon$ /Resources/icon.ico��5   	   .��5      �  � RowDefinitions 5   
   �� 5      $��30 �6   �� 5      $	��600 �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $	��600 �6   �� 5      $	��300 �6   .+�5   
   c� -   5        $��
TopToolbar��5      $��30��6$   $	��900��60   $��0��6<   $��0��6I   $��2��6Y   
�������.@�5      �� -   5        $��OpenFileBtn��5      
�������$��30��6U   $	��100��6a   .��6m   Otwórz plik5      �� 5      -   5        $��ChangeBackgroundBtn��5      
�������$��30��6e   $	��100��6q   .��6}   Zmień tło5      	� 5       -   5        $��ContourPointBtn��5       $��10, 0q�61    	� 	GroupName$ Objects��6A   $	��100��5!      $��30��6%   
�������.��6a   Kontur5#      	� 5$      -   5        $��KeyPointBtn��5$      $
��0, 0q�6-   $ Objects��6;   $	��100��5%      $��30��6%   
�������.��6b   Punkt Kluczowy5'      	� 5(      -   5        $��StatsBtn��5(      $��10, 0q�6*   $ Objects��69   $	��100��5)      $��30��6%   
�������.��6_   
Statystyki5+      5,      �� 5.   
   -   5        $	��Map��5.      $	��600��6   $	��600��6�   $��1��6�   $��0��6�   ��5/      �� 50       �� ImageSource$ /Resources/contour.jpg��6   52      �� 54   
   -   5        $��SideToolbar��54      $	��600��6(   $	��300��65   $��1��6A   $��1��6N   
�������.+�55      ��  ��Language$ pl�57      -	   5        $��UpperSideToolbarTxt��55   $   $��2,2,2,0q�6?   
�������$	��296��6g   $	��302��6s   	 �� TextWrapping$
	 Wrap=�6�   $��Italic$�6�   |�.  $��Disabled=�56      $
��Auto=�57      .��6F   |zKliknij lewym przyciskiem myszy na jeden z punktów kluczowych przy wybranym trybie statystyk, aby wyświetlić statystyki59      �� 5:      $ pl�5<      -
   5        $��LowerSideToolbarTxt��5:   $   $��2,0,2,0q�6?   
�������$	��296��6g   $	��294��6s   $
	 Wrap=�6�   $��Italic$�6�   |�.  $��Disabled=�5;      $
��Auto=�5<      .��6F   5>      5?      �� 5A   
   -   5        $��	OutputTxt��5A      $��38��6#   $	��900��6/   $��0��6;   $��2��6K   $��2��6X   |�.  $
 �� HorizontalScrollBarVisibility$
 Visible=�5B   $   " �� VerticalScrollBarVisibility$
 Auto=�6L   $
	 Wrap=�5C      
���iii�.��6;   5E      5G      5I      