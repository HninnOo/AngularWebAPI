import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service';

@Component({
  selector: 'app-show-dep',
  templateUrl: './show-dep.component.html',
  styleUrls: ['./show-dep.component.css']
})
export class ShowDepComponent implements OnInit {

  constructor(private svc:SharedService) { }

  DepartmentList:any=[];
  ModalTitle:string="";
  ActivateAddEditDepComp:boolean=false;
  dep:any;

  ngOnInit(): void {
    this.refreshDepList();
  }

  addClick(){
    this.dep = {
      DepartmentId:0,
      DepartmentName:""
    }
    this.ModalTitle = "Add Department";
    this.ActivateAddEditDepComp = true;
  }

  editClick(item: any){
    this.dep = item;
    this.ModalTitle = "Edit Department";
    this.ActivateAddEditDepComp = true;
  }

  deleteClick(item: any){
    if(confirm('Are you sure?')){
        this.svc.deleteDepartment(item.DepartmentId).subscribe(data=>{
          alert(data.toString());
          this.refreshDepList();
        });
    }
  }

  closeClick(){
    this.ActivateAddEditDepComp = false;
    this.refreshDepList();
  }

  refreshDepList(){
    this.svc.getDepList().subscribe(data =>{ 
      this.DepartmentList = data;
     });
  }
}
