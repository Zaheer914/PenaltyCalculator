import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import  {HomeComponent} from './home/home.component'
import { InputComponent } from './input/input.component';
import { InstructionsComponent } from './instructions/instructions.component';
const routes: Routes = [
  { path:'home',component:HomeComponent},
  {path:'input',component:InputComponent},
  {path:'instructions',component:InstructionsComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
