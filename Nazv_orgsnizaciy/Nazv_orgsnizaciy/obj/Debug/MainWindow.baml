   M S B A M L   `   `   `  ����K  GNazv_orgsnizaciy, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Nazv_orgsnizaciyNazv_orgsnizaciy  5   	   !    Nazv_orgsnizaciy.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:Nazv_orgsnizaciy     5   	   -   5          �Name$
  Root��5	   	   $��Arial Black)�5   	    � Title$* $Услуги авто сервиса��5
   	   $	��300��5   	   $	��300��6   $	��450��5   	   $
��1100��6   .��5      �  � ColumnDefinitions 5   
   �� 5      $	��150 �6   �� 5      $��1* �6   .+�5   
   ��  �� Orientation$ Vertical=�5      $��5q�5      $��Bottom=�5      .+�5      �� -   5        $#��Добавить услугу��5      y�5      �� *AdminVisibility+68   �� 5      -   5        $:��4Посмотреть Ближайшие записи��5      y�5      �� *AdminVisibility+�� 5       -   5        $��AdminButton��5"      $��16��5$      ��5!      �� *AdminModeCaption+5$      �� 5%      -   5         ��Name$ 
ExitButton��5&      $��
Выход��5'         BrownButtonStyle#��[  5)      5*      �� 5+   
   $ Vertical=�5,      $��Center=�5-      $	��212��6(   $��
0,145,0,62q�65   .+�5.      �� $��
Цена: ��6   	� 5/      -   5         	� 	GroupName$ Price��50   	   
 � Tag$ 1��51   	   $!��по возрастанию��52   	    d� 	IsChecked$
 TrueY�53   	   $��Center=�55   	   	� 56      -   5        $ Price��57   	   $ 2��58   	   $��по убыванию��59   	   $��Center=�5;   	   �� 5=      $&�� Фильтр по скидке: ��6   $��10,0,0,0q�5>   	   $��Center=�5?   	   �� 5@      -   5        $��DiscountFilterComboBox��5A   
    �� SelectedIndex$ 0��5B   
   ��5D   	   �� *FilterByDiscountNamesList+�� 5E      $��
Поиск��6   �� 5F      -	   5        	 ��Name$	 SearchFilterTextBox��5G      $��Center=�5H      5I      �� 5J      ��6   �� *ServicesCount+�� 5K      ��6   �� *FilteredServicesCount+5L      �� 5M   
   $��5q�5N   	   $#��./NewFolder1/service_logo.png��5O   	   $	��Top=�5P   	   &   System.Windows.Controls.DataGrid  5Q   
   -
   5        
  Name$
 MainDataGrid��5R      $��1��5S         CanUserAddRows .     AutoGenerateColumns .  ��5T      �� *ServiceList+   RowStyle 5W      �� 5X       �� 
TargetType)  #System.Windows.Controls.DataGridRow"  6    �� Triggers 5Y      �� 5Z       �� Binding 5[      �� *HasDiscount+ �� Value	 False5\      .a�5]      ��  �� Property � �� �� Value
 ����5_      5`      5b         Columns 5d      0  *System.Windows.Controls.DataGridTextColumn  5e      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$	 250 5f         Header$+ %Наименование услуги��5g      1  +System.Windows.Controls.DataGridBoundColumn   Binding 5h      �� *Title+4  .System.Windows.Controls.DataGridTemplateColumn  5i      $	 100 5j      $ Стоимость��5k         CellTemplate 5l      �� 5m      .R�5n      �� ��5o      �� *CostTextDecoration+��5p      �� *
CostString+5q      5s        5v      $	 150 5w      $( "Продолжительность��5x       5y      �� *DurationInSeconds+  5z      $ 64 5{      $ Картинки��5|       5}      �� 5~      .R�5      �� $��64��5�   !   $��64��5�   !   ��5�   !   �� *
ImageUri+5�      5�        5�      $ 60 5�      $ Скидка��5�       5�      �� *DiscountString+  5�      $	 100 5�      $, &Стоимость со скидкой��5�       5�      �� *CostWithDiscount+  5�      $ Действия��5�       5�      �� 5�      .R�5�      �� $ 
Horizontal=�5�      y�5�      �� *DataContext.AdminVisibility+ �� ElementName$
 Root��.+�5�   "   �� $��
EditButton��5�   !   -   5        $ ��Редактировать��5�   !   5�   3   �� 5�   "   $��SubscrideButton��5�   )   -   5        $��Записаться��5�   )   5�   )   �� 5�   "   $��DeleteButton��5�   !   -   5        $��Удалить��5�   !   5�   5   5�      5�      5�      5�      5�      5�      