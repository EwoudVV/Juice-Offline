// Assets/Plugins/JuiceAPI.js

mergeInto(LibraryManager.library, {
    CallJSFunction: function() {
        console.log("JavaScript function called from Unity!");
    }
});