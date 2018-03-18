﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ICSharpCode.TreeView;
using OdraIDE.Core;
using OdraIDE.Utilities;

namespace OdraIDE.SolutionExplorer.Connections
{
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Export("DatabaseNode")]
    public class DatabaseNode : SharpTreeNode
    {
        [Import(CompositionPoints.Workbench.Commands.RenameDatabase, typeof(ICustomCommand))]
        private RenameDatabaseCommand renameDatabaseCommand { get; set; }

        public string DatabaseName { get; set; }

        public DatabaseNode()
        {
            ShowIcon = true;
        }

        public override object Text
        {
            get
            {
                return DatabaseName;
            }
        }

        public override object Icon
        {
            get
            {
                return ImageHelper.GetImageFromResources(Resources.Images.Database);
            }
        }

        private ContextMenu m_menu;

        public override ContextMenu GetContextMenu()
        {
            if (m_menu == null)
            {
                m_menu = new ContextMenu();
                MenuItem addDatabaseItem = new MenuItem();
                renameDatabaseCommand.DatabaseName = DatabaseName;
                addDatabaseItem.Command = renameDatabaseCommand;
                addDatabaseItem.Header = "Rename database";
                m_menu.Items.Add(addDatabaseItem);
            }
            return m_menu;
        }
    }
}
