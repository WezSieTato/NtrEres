   M S B A M L   `   `   `  ����?  ;Eres, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Eres.View	Eres.View  5      /clr-namespace:Eres.ViewModelEres.ViewModel  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   ButtonStyle  �� 
TargetType �� Property �� Value  ButtonBackground �� 	Resources  textBoxInError �� Triggers P� Property P� Value �� Self �� RelativeSource	 �� Path  EresDataGrid&    System.Windows.Controls.DataGrid  GridLineColorBrush  AlternateRowBackgroundBrush  ColumnHeaderStyle=  7System.Windows.Controls.Primitives.DataGridColumnHeader  ColumnHeaderBackground  TabBackground 	 TabItemBackground
 �� Color �� 
StartPoint �� EndPoint � Color � Offset 
 	EditImage �� 	UriSource  AddImage  DeleteImage�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      vclr-namespace:Eres.View  5      #vmclr-namespace:Eres.ViewModel  5      %�
  &	        &	     &	 �    &	 C    '�� �    '�� �    &	     &	 8    &	 �    &	 �    &		 F    &	
 	    &	 ^	    &	 �	    �� 5      "  ��6    .�5   
   ��  � ��
 �����6(   �� 5   
    � ��# �  6'   �� 5	   
    � ��$ 4q�6$   �� 5
   
    � ��$ 10,10,10,10q�6#   �� 5   
    � ��$	 120��6%    5   
   ���� 5      '��       "  ��6   .�5      ��  � ��$ 24��6*   �� 5       � ��$ 24��6+   �� 5       � ��$ 0,0,3,0q�6+   �� 5       � ��$
 Left=�68   5      �� 5      '��       "  ��6   .�5      ��  � ��$ Center=�66   5      5      5      �� 5      "  ��6#    5   
   P� 5       �@=�HasError . . �5      ��  �@�ToolTip 5      �� # Z $)	 #(Validation.Errors)[0].ErrorContent�5      5!      �� 5#      "    6!   .�5$   
   ��  �@  CanUserAddRows .  6+   �� 5%   
    �@  CanUserDeleteRows .  6.   �� 5&   
    �@  SelectionMode$ Single=�6*   �� 5'   
    �@  AutoGenerateColumns .  60   �� 5(   
    � ��
 �����6'   �� 5)   
    � ��$
 Auto=�6G   �� 5*   
    � ��$
 Auto=�6E   �� 5+   
    �@  RowHeaderWidth$ 0Q�6+   �� 5,   
    � ��$ 0q�6#   �� 5-   
     �@  HorizontalGridLinesBrush# �  65   �� 5.   
    �@  VerticalGridLinesBrush# �  63   �� 5/   
     �@  AlternatingRowBackground# �  65   50      �� 52      "   6&   .�53   
   ��  � ��# �  6'   54      �� 56      "  ��6(   .�57   
   ��  � ��# �  6'   58      �� 5:      "  ��6   .�5;   
   ��  � ��# � 	 6'   5<      �� 5>      $
 	#FFCEDFF6��61   �� 5?      $ 0.5,0.0&�5@      $ 0.5,1.0&�6+   .��5A   
   � $ 	#FFFEFEFF��6   $ 0Q�6)   � 5B   
   $ 	#FFE4F0FC��6   $ 1Q�6)   5C      �� 5E      $ 0.5,0.0&�5F      $ 0.5,1.0&�6+   .��5G   
   � $ 
WhiteSmoke��6   $ 0Q�6*   � 5H   
   $ 	#FFC2C2C2��6   $ 1Q�6)   5I      �� 5K      $	 0,0&�5L      $	 1,1&�6'   .��5M   
   � $ 	LightGray��6   $ 0Q�6)   � 5N   
   $ White��6   $	 0.3Q�6%   � 5O   
   $
 Gray��6   $ 1Q�6$   5P      �� 5R      $ 0.5,0&�5S      $ 0.5,1&�6)   .��5T   
   � $ 	LightGray��6   $ 0Q�6)   � 5U   
   $ White��6   $	 0.5Q�6%   � 5V   
   $
 Gray��6   $ 1Q�6$   5W      �� 5Y      $; 5pack://application:,,,/Eres;component/Images/Edit.png?�6$   �� 5Z      $: 4pack://application:,,,/Eres;component/Images/Add.png?�6#   �� 5[      $= 7pack://application:,,,/Eres;component/Images/Delete.png?�6&   5]      