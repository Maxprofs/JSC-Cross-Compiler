
using android.app;
using android.content;
using android.database;
using android.database.sqlite;
using android.provider;
using android.webkit;
using android.widget;
using AndroidSQLiteActivity.Library;
using ScriptCoreLib;
using ScriptCoreLib.Android;

namespace AndroidSQLiteActivity.Activities
{
    public class AndroidSQLiteActivity : Activity
    {
        // inspired by http://android-er.blogspot.com/2011/06/simple-example-using-androids-sqlite.html

        // C:\util\android-sdk-windows\tools\android.bat create project --package AndroidSQLiteActivity.Activities --activity AndroidSQLiteActivity  --target 2  --path y:\jsc.svn\examples\java\android\AndroidSQLiteActivity\AndroidSQLiteActivity\staging\


        // running it in emulator:
        // start C:\util\android-sdk-windows\tools\android.bat avd
        // "C:\util\android-sdk-windows\platform-tools\adb.exe" install -r  "y:\jsc.svn\examples\java\android\AndroidSQLiteActivity\AndroidSQLiteActivity\staging\bin\AndroidSQLiteActivity-debug.apk"

        // note: rebuild could auto reinstall

        // running it on device:
        // attach device to usb
        //Z:\jsc.svn\examples\java\android\HelloAndroid>C:\util\android-sdk-windows\platform-tools\adb.exe devices
        //List of devices attached
        //3330A17632C000EC        device 
        private SQLiteAdapter mySQLiteAdapter;

        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {

            base.onCreate(savedInstanceState);
            setContentView(R.layout.main);
            TextView listContent = (TextView)findViewById(R.id.contentlist);

            /*
             *  Create/Open a SQLite database
             *  and fill with dummy content
             *  and close it
             */
            mySQLiteAdapter = new SQLiteAdapter(this);
            mySQLiteAdapter.openToWrite();
            mySQLiteAdapter.deleteAll();
            mySQLiteAdapter.insert("ABCDE");
            mySQLiteAdapter.insert("FGHIJK");
            mySQLiteAdapter.insert("1234567");
            mySQLiteAdapter.insert("890");
            mySQLiteAdapter.insert("Testing");
            mySQLiteAdapter.close();

            /*
             *  Open the same SQLite database
             *  and read all it's content.
             */
            mySQLiteAdapter = new SQLiteAdapter(this);
            mySQLiteAdapter.openToRead();
            var contentRead = mySQLiteAdapter.queueAll();
            mySQLiteAdapter.close();

            listContent.setText(contentRead);


            this.ShowToast("http://jsc-solutions.net");
        }

        public class SQLiteAdapter
        {

            public const string MYDATABASE_NAME = "MY_DATABASE";
            public const string MYDATABASE_TABLE = "MY_TABLE";
            public const int MYDATABASE_VERSION = 1;
            public const string KEY_CONTENT = "Content";

            //create table MY_DATABASE (ID integer primary key, Content text not null);
            private const string SCRIPT_CREATE_DATABASE =
             "create table " + MYDATABASE_TABLE + " ("
             + KEY_CONTENT + " text not null);";

            private SQLiteHelper sqLiteHelper;
            private SQLiteDatabase sqLiteDatabase;

            private Context context;

            public SQLiteAdapter(Context c)
            {
                context = c;
            }

            public SQLiteAdapter openToRead() /* throws android.database.SQLException */ {
                sqLiteHelper = new SQLiteHelper(context, MYDATABASE_NAME, null, MYDATABASE_VERSION);
                sqLiteDatabase = sqLiteHelper.getReadableDatabase();
                return this;
            }

            public SQLiteAdapter openToWrite() /* throws android.database.SQLException */ {
                sqLiteHelper = new SQLiteHelper(context, MYDATABASE_NAME, null, MYDATABASE_VERSION);
                sqLiteDatabase = sqLiteHelper.getWritableDatabase();
                return this;
            }

            public void close()
            {
                sqLiteHelper.close();
            }

            public long insert(string content)
            {

                ContentValues contentValues = new ContentValues();
                contentValues.put(KEY_CONTENT, content);
                return sqLiteDatabase.insert(MYDATABASE_TABLE, null, contentValues);
            }

            public int deleteAll()
            {
                return sqLiteDatabase.delete(MYDATABASE_TABLE, null, null);
            }

            public string queueAll()
            {
                var columns = new [] { KEY_CONTENT };
                Cursor cursor = sqLiteDatabase.query(MYDATABASE_TABLE, columns,
                  null, null, null, null, null);

                var result = new java.lang.StringBuilder();

                int index_CONTENT = cursor.getColumnIndex(KEY_CONTENT);
                for (cursor.moveToFirst(); !(cursor.isAfterLast()); cursor.moveToNext())
                {
                    result.append( cursor.getString(index_CONTENT)).append( "\n");
                }

                return result.ToAndroidString();
            }

            public class SQLiteHelper : SQLiteOpenHelper
            {

                public SQLiteHelper(Context context, string name, android.database.sqlite.SQLiteDatabase.CursorFactory factory, int version)
                    : base(context, name, factory, version)
                {

                }

                public override void onCreate(SQLiteDatabase db)
                {
                    // TODO Auto-generated method stub
                    db.execSQL(SCRIPT_CREATE_DATABASE);
                }

                public override void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
                {
                    // TODO Auto-generated method stub

                }

            }

        }
    }
}
