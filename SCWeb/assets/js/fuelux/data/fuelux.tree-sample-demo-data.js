var DataSourceTree = function(options) {
	this._data 	= options.data;
	this._delay = options.delay;
}

DataSourceTree.prototype.data = function(options, callback) {
	var self = this;
	var $data = null;

	if(!("name" in options) && !("type" in options)){
		$data = this._data;//the root tree
		callback({ data: $data });
		return;
	}
	else if("type" in options && options.type == "folder") {
		if("additionalParameters" in options && "children" in options.additionalParameters)
			$data = options.additionalParameters.children;
		else $data = {}//no data
	}
	
	if($data != null)//this setTimeout is only for mimicking some random delay
		setTimeout(function(){callback({ data: $data });} , parseInt(Math.random() * 500) + 200);

	//we have used static data here
	//but you can retrieve your data dynamically from a server using ajax call
	//checkout examples/treeview.html and examples/treeview.js for more info
};

var tree_data = {
    'for-sale': { name: '系统管理', type: 'folder' },
    'vehicles': { name: '商品管理', type: 'folder' },
	'tickets' : {name: '退换货管理', type: 'item'}	,
	'services' : {name: '采购管理', type: 'item'}	,
	'personals' : {name: '财务管理', type: 'item'}
}
tree_data['for-sale']['additionalParameters'] = {
	'children' : {
		'appliances' : {name: '机构管理', type: 'item'},
		'arts-crafts' : {name: '部门管理', type: 'item'},
		'clothing' : {name: '员工管理', type: 'item'},
		'computers' : {name: '资源管理', type: 'item'},
		'jewelry' : {name: '用户管理', type: 'item'},
		'office-business' : {name: '角色管理', type: 'item'},
		'sports-fitness' : {name: '用户解锁', type: 'item'}
	}
}
tree_data['vehicles']['additionalParameters'] = {
	'children' : {
		'cars' : {name: '机构管理', type: 'folder'},
		'motorcycles' : {name: '部门管理', type: 'item'},
		'boats' : {name: '员工管理', type: 'item'}
	}
}
tree_data['vehicles']['additionalParameters']['children']['cars']['additionalParameters'] = {
	'children' : {
		'classics' : {name: '机构管理', type: 'item'},
		'convertibles' : {name: '部门管理', type: 'item'},
		'coupes' : {name: '员工管理', type: 'item'},
		'hatchbacks' : {name: '资源管理', type: 'item'},
		'hybrids' : {name: '用户管理', type: 'item'},
		'suvs' : {name: '角色管理', type: 'item'},
		'sedans' : {name: '用户解锁', type: 'item'}
	}
}

var treeDataSource = new DataSourceTree({data: tree_data});
