   M S B A M L   `   `   `  ����B  >EresWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Eres.View	Eres.View  5      /clr-namespace:Eres.ViewModelEres.ViewModel  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   ButtonStyle  �� 
TargetType �� Property �� Value �� 	Resources  textBoxInError �� Triggers P� Property P� Value �� Self �� RelativeSource	 �� Path  EresDataGrid&    System.Windows.Controls.DataGrid  GridLineColorBrush  AlternateRowBackgroundBrush  ColumnHeaderStyle=  7System.Windows.Controls.Primitives.DataGridColumnHeader�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      vclr-namespace:Eres.View  5      #vmclr-namespace:Eres.ViewModel  5      %�  &	        &	 
    &	 �    &	 6    '�� W    '�� x    �� 5      "  ��6    .�5   
   ��  � ��$ 4q�6$   �� 5	   
    � ��$ 10,10,10,10q�6#   �� 5
   
    � ��$	 120��6%    5   
   ���� 5      '��       "  ��6   .�5      ��  � ��$ Center=�66   5      5      5      �� 5      "  ��6#    5   
   P� 5       �@=�HasError . . �5      ��  �@�ToolTip 5      �� # Z $)	 #(Validation.Errors)[0].ErrorContent�5      5      �� 5      "    6!   .�5   
   ��  �@  CanUserAddRows .  6+   �� 5   
    �@  CanUserDeleteRows .  6.   �� 5    
    �@  SelectionMode$ Single=�6*   �� 5!   
    �@  AutoGenerateColumns .  60   �� 5"   
    � ��
 �����6'   �� 5#   
    � ��$
 Auto=�6G   �� 5$   
    � ��$
 Auto=�6E   �� 5%   
    �@  RowHeaderWidth$ 0Q�6+   �� 5&   
    � ��$ 0q�6#   �� 5'   
     �@  HorizontalGridLinesBrush# �  65   �� 5(   
    �@  VerticalGridLinesBrush# �  63   �� 5)   
     �@  AlternatingRowBackground# �  65   5*      �� 5,      "   6&   5-      �� 5/      "  ��6(   50      �� 52      "  ��6   53      57      