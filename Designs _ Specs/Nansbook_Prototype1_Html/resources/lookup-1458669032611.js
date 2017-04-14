(function(window, undefined) {
  var dictionary = {
    "3c6c0785-b66a-4422-b719-d40ffcc1b06e": "signup screen",
    "cd2bec41-ae4b-4e8d-995b-629af628ec6f": "technicians add screen",
    "7bfb0da9-56b9-4385-ab35-29e594a28270": "home screen",
    "704afe8d-e29f-4220-977e-cefdc92b8ddd": "profile screen",
    "7627d86d-dabe-4858-b696-02f2d38ba421": "payment methods screen",
    "74f2b088-a6fe-4d17-b5bc-66a2581f0cfc": "forgot username SUCCESS screen",
    "50fc836c-7a1f-4b83-8f2d-0111c9fc79e9": "settings screen",
    "69670012-be2f-472d-b56a-a6748d20c1be": "services screen",
    "5193f933-ce64-451e-8fa8-eef083d92bac": "services add screen",
    "00190b05-9fb4-438d-8f83-06fa1a7c7ed2": "signup email sent screen",
    "f1acddb1-ad1f-4fe1-96df-849d25cb69dc": "expense categories screen",
    "38dfb20f-a088-439a-8f55-4cdc41ea2550": "notifications screen",
    "7d72a1ae-7848-4200-aaa9-278fdeaaa844": "products screen",
    "3801b70c-4e81-491f-9bf6-cc3c30b2fc54": "technicians edit screen",
    "1432c19d-c891-4d09-9d9e-a0e9b91f9494": "login screen",
    "2501923f-c090-43be-a2f4-e17c278135e9": "forgot username screen",
    "4d86669e-0022-48de-93ab-383162bd19d7": "technicians screen",
    "f39803f7-df02-4169-93eb-7547fb8c961a": "Template 1",
    "bb8abf58-f55e-472d-af05-a7d1bb0cc014": "default"
  };

  var uriRE = /^(\/#)?(screens|templates|masters|scenarios)\/(.*)(\.html)?/;
  window.lookUpURL = function(fragment) {
    var matches = uriRE.exec(fragment || "") || [],
        folder = matches[2] || "",
        canvas = matches[3] || "",
        name, url;
    if(dictionary.hasOwnProperty(canvas)) { /* search by name */
      url = folder + "/" + canvas;
    }
    return url;
  };

  window.lookUpName = function(fragment) {
    var matches = uriRE.exec(fragment || "") || [],
        folder = matches[2] || "",
        canvas = matches[3] || "",
        name, canvasName;
    if(dictionary.hasOwnProperty(canvas)) { /* search by name */
      canvasName = dictionary[canvas];
    }
    return canvasName;
  };
})(window);