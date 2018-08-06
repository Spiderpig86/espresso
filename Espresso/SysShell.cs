using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Espresso {
    class SysShell {
        private const BindingFlags PublicInstance =
            BindingFlags.Public | BindingFlags.Instance;

        private Type type;
        private object shell;

        public SysShell() {
            this.type = Type.GetTypeFromProgID("WScript.Shell");
            this.shell = Activator.CreateInstance(type);
        }

        public object CreateShortcut(
            string linkFileName,
            string targetPath,
            string workingDir = null

        ) {
            object shortcut = type.InvokeMember(
                "CreateShortcut", PublicInstance | BindingFlags.InvokeMethod,
                null, shell, new object[] { linkFileName }
            );

            Type shortcutType = shortcut.GetType();
            shortcutType.InvokeMember(
                "TargetPath", PublicInstance | BindingFlags.SetProperty,
                null, shortcut, new object[] { targetPath }
            );

            if (workingDir != null) {
                shortcutType.InvokeMember(
                    "WorkingDirectory",
                    PublicInstance | BindingFlags.SetProperty,
                    null, shortcut, new object[] { workingDir }
                );
            }

            shortcutType.InvokeMember(
                "Save", PublicInstance | BindingFlags.InvokeMethod,
                null, shortcut, null
            );

            return shortcut;
        }

        public string GetSpecialFolder(string item) {
            object specFolders = type.InvokeMember(
                "SpecialFolders", PublicInstance | BindingFlags.GetProperty,
                null, shell, null
            );

            Type specFoldersType = specFolders.GetType();
            object path = specFoldersType.InvokeMember(
                "Item", PublicInstance | BindingFlags.InvokeMethod,
                null, specFolders, new object[] { item }
            );

            return path as string;
        }
    }
}
